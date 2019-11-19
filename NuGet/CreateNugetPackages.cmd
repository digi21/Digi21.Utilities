@echo off
nuget.exe pack Digi21.Utilities.nuspec
nuget.exe sign Digi21.Utilities.17.2.0.nupkg -CertificatePath dwo.pfx -TimeStamper http://tsa.starfieldtech.com
