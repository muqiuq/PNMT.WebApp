$registryName = Read-Host "name (registry.example.com/user/app)"
$version = Read-Host "Version (without v at beginning)"

$folderIdentifyFile = "Dockerbuild.PNMT.WebApp.sln"
$changePathBack = $false
$foundPath = $true
$directory = $(Get-Location)

for (($i = 0), ($j = 0); $i -lt 10; $i++) {
	if(-not(Test-Path -Path $folderIdentifyFile -PathType Leaf)) {
		Write-Host "Not in right directory changing path"
		Set-Location ..
		$changePathBack = $true
	}else{
		Write-Host "Found right path with $folderIdentifyFile in it"
		$foundPath = $true;
		break
	}
}

if(-not($foundPath)) {
	Write-Host "Did not find right path. abort"
	exit 3
}

Write-Host "Copying Dockerfile"
Copy-Item -Path "PNMT.WebApp/PNMT.WebApp/Dockerfile" -Destination "."

$cmd_build = "docker build -t $($registryName):v$version ."
$cmd_push = "docker push $($registryName):v$version"

Write-Host $cmd_build
Write-Host $cmd_push

$confirm = Read-Host "Confirm? (y/n)"

if($confirm -eq "y") {
	Invoke-Expression $cmd_build
	Invoke-Expression $cmd_push
	Write-Host "Done"
}

if($changePathBack) {
	Write-Host "Changing back"
	Set-Location $directory
}

