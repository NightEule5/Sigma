# Sigma Plans

Here I will lay out how some plans I have for Sigma and whether they are finished, being worked on, or merely on paper.

## Structure

The Sigma build system will have a stucture based on compartmentalized components of a build. These components are: Tasks, steps, and dependency resolvers. These are explained in more detail below.

### Tasks

Tasks perform actions on the build and return values to be used in consecutive tasks. They provide varying degrees of abstraction, where you can use tasks written for you (included in Sigma) or create your own in C#.

Here is an example for compiling C++ source files with clang:  
```javascript
{
    "Type": "Sigma.Build.BasicCppCompileTask",
    "Parameters": [
        "Compiler": "Clang",
        "Options": [
            "Standard": "c++2a", // -std=c++2a
            "Warnings": "all",   // -Wall
            "Optimization": 3,   // -O3
            "Definitions": [
                "Version": "1.7.10", // -DVersion=1.7.10
                "DllImports": true   // -DDllImports
            ]
            // Options can also be defined as the CLI would see them, like this:
            // "O": 3
        ], // Could also be defined like this: "Options": "-std=c++2a -Wall -O3"
        "Blacklist": [ "DontTouchMeh!.cpp" ]
    ]
}
```

This task would compile, using clang, all source files under the `Sources` variable that aren't in its `Blacklist` variable (DontTouchMeh!.cpp). Each source would would be compiled with these options: `-std=c++2a -Wall -O3 -DVersion=1.7.10 -DDllImports`. The options specified are considered overrides by Sigma, so more options may be specified to the compiler.

### Dependency Resolvers

Dependency Resolvers (or simply "Resolvers") gather and build anything the build depends on. For example, a dependency could be a git repository that is cloned and built on every build. In the case of C/C++ builds, these should also update the `CIncludes` or `CppIncludes` build variables with include paths for the compiler.

The location where dependencies are stored is determined by Sigma. Sigma may or may not cache dependencies, so it's important to program custom resolvers to avoid resolving a current depencency again where possible in order to avoid unnecessary slow-downs.

This very simple example shows a cloned git repository:
```javascript
{
    "Type": "Sigma.Build.GitResolver",
    "Uri": "https://github.com/nlohmann/json.git",
    "Includes": [
        { "Directory": "single_include/nlohmann", "Language": "C++" /* Also: "Languages": [ "C++" ] */ }
    ]
}
```

This resolver definition would clone nlohmann's json.hpp repository and add it to the C++ include search path.

This example can also be written in a shorthand version (syntax not set-in-stone):
```javascript
"https://github.com/nlohmann/json.git:Includes[single_include/nlohmann(C++)]"
```

Sigma will automatically recognise that this is a git repository and act appropriately.
