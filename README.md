# Cake.CodeQL.Cli
CodeQL Scanning from GitHub is the analysis engine used by developers to automate security checks, and by security researchers to perform variant analysis. 

This plugin is a set of Cake aliases for [GitHub CodeQL CLI](https://docs.github.com/en/code-security/code-scanning/using-codeql-code-scanning-with-your-existing-ci-system/about-codeql-code-scanning-in-your-ci-system) (.NET Core or .NET6 or greater) used for scanning code hosted on GitHub or GitHub Enterprise when GitHub Actions is not an option.

> :exclamation: Please read the [GitHub CodeQL Terms and Conditions](https://github.com/github/codeql-cli-binaries/blob/main/LICENSE.md) before considering using this plugin.

## Prerequisites
  - [CodeQL CLI installed on CI agent host machine](https://docs.github.com/en/code-security/code-scanning/using-codeql-code-scanning-with-your-existing-ci-system/installing-codeql-cli-in-your-ci-system)
  - Compliance with [GitHub CodeQL Terms and Conditions](https://github.com/github/codeql-cli-binaries/blob/main/LICENSE.md)
  
## Using Cake Projects

### Cake Script
```csharp
#addin "nuget:?package=Cake.CodeQL.Cli"
```
### Cake Frosting Project
```xml
<PackageReference Include="Cake.CodeQL.Cli" Version="4.0.0" />
```

## Discussion

If you have questions, search for an existing one, or create a new discussion on the Cake GitHub repository, using the `extension-q-a` category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## License (this plugin only)

[![License](http://img.shields.io/:license-mit-blue.svg)](http://cake-contrib.mit-license.org)