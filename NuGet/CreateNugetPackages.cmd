@echo off
nuget.exe pack Digi21.Utilities.nuspec
nuget.exe sign Digi21.Utilities.21.0.0.nupkg -CertificateSubjectName "DREAMING WITH OBJECTS" -TimeStamper http://tsa.starfieldtech.com
