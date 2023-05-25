# Preface:
# dotnet tool install -g will return an error code when the tool is already installed in the system (at the same location)
# adding a test like below, will prevent the error
# this is mostly needed in a CI/CD environment where you don't want to break your pipeline if the tool was installed already.
# this code is referenced from https://gist.github.com/rajbos/b148e9833a5d08165188dbe00cc32301

# list all global tools
$list = (dotnet tool list -g)

# this is true if the list has an entry starting with "nuke.globaltool"
$nukeInstalled = ($list | Where-Object {$_.StartsWith("nuke.globaltool") }).Count -gt 0

if ($false -eq $nukeInstalled) {
  Write-Host "Installing Nuke.."
  dotnet tool install -g nuke.globaltool
}
else {
  Write-Host "Nuke is already installed, trying to update"
  dotnet tool update -g nuke.globaltool
}
