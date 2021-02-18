using NUnit.Framework;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("CCC-API")]
[assembly: AssemblyDescription( "Features, Steps and Services for testing the C3 API" )]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany( "Cision" )]
[assembly: AssemblyProduct( "C3" )]
[assembly: AssemblyCopyright( "Copyright ©  2018" )]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("8b0af437-c0e2-4f45-8f19-007f2a497a90")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.19.0")]
[assembly: AssemblyFileVersion("1.0.19.0")]

// Following required for parallel test execution
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]
