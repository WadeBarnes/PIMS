import groovy.json.JsonOutput
import groovy.json.JsonSlurper
import java.util.regex.Pattern

def version = "1.0"

// ------------------
// Git Functions
// ------------------

// Determine whether there were any changes the files within the project's context directory.
@NonCPS
boolean hasDirectoryChanged(String contextDirectory) {
  // Determine if code has changed within the source context directory.
  def changeLogSets = currentBuild.changeSets
  def filesChangeCnt = 0
  for (int i = 0; i < changeLogSets.size(); i++) {
    def entries = changeLogSets[i].items
    for (int j = 0; j < entries.length; j++) {
      def entry = entries[j]
      def files = new ArrayList(entry.affectedFiles)
      for (int k = 0; k < files.size(); k++) {
        def file = files[k]
        def filePath = file.path
        if (filePath.contains(contextDirectory)) {
          filesChangeCnt = 1
          k = files.size()
          j = entries.length
        }
      }
    }
  }

  if ( filesChangeCnt < 1 ) {
    echo('The changes do not require a build.')
    return false
  }
  else {
    echo('The changes require a build.')
    return true
  }
}

/*
 * Generates a string containing all the commit messages from
 * the builds in pastBuilds.
 */
 String getChangeLog() {
  MAX_MSG_LEN = 512
  def changeString = ""
  def changeLogSets = currentBuild.changeSets
  for (int i = 0; i < changeLogSets.size(); i++) {
     def entries = changeLogSets[i].items
     for (int j = 0; j < entries.length; j++) {
         def entry = entries[j]
         truncated_msg = entry.msg.take(MAX_MSG_LEN)
         changeString += " - ${truncated_msg} [${entry.author}]\n"
     }
  }
  if (!changeString) {
     changeString = "No changes"
  }
  return changeString
}

// ------------------
// Openshift Functions
// ------------------

// Get an image's hash tag
String getImageTagHash(String imageName, String tag = "") {
  if(!tag?.trim()) {
    tag = "latest"
  }
  def istag = openshift.raw("get istag ${imageName}:${tag} -o template --template='{{.image.dockerImageReference}}'")
  return istag.out.tokenize('@')[1].trim()
}

// Build the objects that are found by the filter.
def build(buildconfigs, int waitTimeout) {
  echo "Build in script"
  // Find all of the build configurations associated to the application ...
  echo "Found ${buildconfigs.count()} buildconfigs: ${buildconfigs.names()}"

  // Kick off all the builds in parallel ...
  def builds = buildconfigs.startBuild()
  echo "Started ${builds.count()} builds: ${builds.names()}"

  timeout(waitTimeout) {
    // Wait for all the builds to complete ...
    // This section will exit after the last build completes.
    echo "Waiting for builds to complete ..."
    builds.withEach {
      // untilEach and watch - do not support watching multiple named resources,
      // so we have to feed it one at a time.
      it.untilEach(1) {
          echo "${it.object().status.phase} - ${it.name()}"
          return (it.object().status.phase == "Complete")
      }
    }
  }

  echo "Builds complete ..."
}

// --------------------
// Supporting Functions
// --------------------

return this;
