nuget pack CCC-API\CCC-API.csproj
REM delete old nuget from local nuget source and copy newly created one
del ..\nuget\CCC-API*.nupkg
rmdir ..\nuget\
mkdir ..\nuget\
copy *.nupkg ..\nuget\