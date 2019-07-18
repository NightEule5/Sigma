![Licence](https://img.shields.io/github/license/NightEule5/Sigma.svg)
 
 # Sigma
 
 ## About the Project

### Origins and Goals

Sigma is a new build system that aims to be a step in the right direction toward fixing flaws in current build systems.

The idea for the project came about when I was working on a C++ project (Peregrine). I had basically no background in C++ up to that point, except for a basic understanding of how it worked. When choosing a build system, I found a problem: They all suck in there own way. I researched the best build tool, and discovered Cmake. But Cmake's syntax was hard to learn and it didn't work 99% of the time (for me anyway). Ninja and Make files are monotonous to write yourself. GN has lack of documentation/examples. Current build systems have their uses, but they also have some flaws that make them a pain to work with.

So one day, being fed up with the lack of a good system, I joked with a friend that I should just build my own. And thus, Sigma was born.

I wanted a system that was versatile, yet intuative to use. Although not as much of a priority, speed would also be nice. Cross-platform builds had to be easy, and language support beyond C and C++ was needed (CUDA, OpenCL, C#, etc).

So, Sigma is meant to include all of these into a comprehensive system. It's still in it's infancy, the code is messy and it doesn't yet work. When finished, I hope it features all of these goals in a genuinely useful manner.

### Structure

Sigma is written in .NET using the .NET Core 2.2 SDK, but will be upgraded to .NET Core 3.0 when it comes out. Due to this, it's cross-platform, supporting Linux, Windows, and MacOS. It can perform cross-platform builds to any of these regardless of the host OS, provided the right compiler is present.

The build scripts are written in C# and are detected and compiled at runtime. The API for writing build files is designed to be easy to use, and it's easy to learn enough C# to get a build working.

It can also easily detect files from other build systems (CMakeLists.txt, Visual Studio solution, etc.) and invoke that build system.

The goal is to be compatible with building not just C/C++-like languages, but also C#, Python, Java, etc.
