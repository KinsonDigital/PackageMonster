<div align="center">
    <a href="#"><img align="center" src="https://raw.githubusercontent.com/KinsonDigital/PackageMonster/master/Documentation/Images/pkg-monster-logo.png" height="96"></a>
    <br />

</div>

<h1 align="center">

**Package Monster**
</h1>

<div align="center">

[![Good First GitHub Issues](https://img.shields.io/github/issues/kinsondigital/PackageMonster/good%20first%20issue?color=7057ff&label=Good%20First%20Issues)](https://github.com/KinsonDigital/PackageMonster/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22)
[![Discord](https://img.shields.io/discord/481597721199902720?color=%23575CCB&label=chat%20on%20discord&logo=discord&logoColor=white)](https://discord.gg/qewu6fNgv7)
</div>

<div align="center">

## **🤷🏼‍♂️ What is it? 🤷🏼‍♂️**
</div>

### This GitHub action checks whether or not a package with a particular name and version exists in a public gallery package repository like [nuget.org](https://www.nuget.org).

<br/>

> **Note** This GitHub action is built using C#/NET and runs in a docker container.  If the job step for running this action is contained in a job that runs on **Windows**, you will need to move the step to a job that runs on **Ubuntu**.  You can split up your jobs to fulfill `runs-on` requirements of the GitHub action. This can be accomplished by moving the step into its own job.  You can then route the action step outputs to the job outputs and use them throughout the rest of your workflow. For more information, refer to the Github documentation links below:
> For more info on step and job outputs, refer to the GitHub documentation links below:
> - [Defining outputs for jobs](https://docs.github.com/en/actions/using-jobs/defining-outputs-for-jobs)
> - [Setting a step action output parameter](https://docs.github.com/en/actions/using-workflows/workflow-commands-for-github-actions#setting-an-output-parameter)

<div align="center"><h2 style="font-weight:bold">🪧 Example 🪧</h2></div>

```yaml
name: Package Monster Action Sample

on:
  workflow_dispatch:

jobs:
  Test_Action:em
    name: Test Package Monster GitHub Action
    runs-on: ubuntu-latest 👈🏼 # Required (Refer to the note above)
    steps:
    - uses: actions/checkout@v3

    - name: Check If Package Exists
      id: nuget-exists
      uses: KinsonDigital/PackageMonster@v1.0.0-preview.1
      with:
        package-name: MyPackage 👈🏻 # Required input
        version: 1.2.3 👈🏻 # Required input
        fail-when-not-found: true 👈🏻 # Optional input

    - name: Print Output Result #PowerShell Core
      shell: pwsh 👈🏼 # Must be explicit with the shell to use PowerShell on Ubuntu
      run: |
        #        Output name for the Package Monster GitHub action 👇🏼
        #                                               _____|_____
        #                                              |          |
        $nugetExists = "${{ steps.nuget-exists.outputs.nuget-exists }}";
        
        if ($nugetExists -eq "true") {
          Write-Host "The package exists!!";
        } else {
          Write-Host "The package does not exist!!";
        }
```

---

<div align="center"><h2 style="font-weight:bold">🪧 Example 🪧</h2></div>

```yaml
name: Package Monster Action Sample

on:
  workflow_dispatch:

jobs:
  Test_Action:em
    name: Test Package Monster GitHub Action
    runs-on: ubuntu-latest 👈🏼 # Required (Refer to the note above)
    steps:
    - uses: actions/checkout@v3

    - name: Check If NPM Package Exists
      id: npm-exists
      uses: KinsonDigital/PackageMonster@v1.0.0-preview.1
      with:
        package-name: MyPackage 👈🏻 # Required input
        version: 1.2.3 👈🏻 # Required input
        source: https://registry.npmjs.org/PACKAGE-NAME
        json-path: $.versions[*].version
        fail-when-not-found: true 👈🏻 # Optional input

    - name: Print Output Result #PowerShell Core
      shell: pwsh 👈🏼 # Must be explicit with the shell to use PowerShell on Ubuntu
      run: |
        #        Output name for the Package Monster GitHub action 👇🏼
        #                                               _____|_____
        #                                              |          |
        $npmExists = "${{ steps.npm-exists.outputs.nuget-exists }}";
        
        if ($nugetExists -eq "true") {
          Write-Host "The package exists!!";
        } else {
          Write-Host "The package does not exist!!";
        }
```

---

<div align="center">

## **➡️ Action Inputs ⬅️**
</div>

| Input Name | Description                                                                           | Required | Default Value |
|---|:-----------------------------------------------------------------------------------------------|:---:|:---:|
| `package-name` | The name of the package.                                                    | yes | N/A |
| `version` | The version of the package.                                                            | yes | N/A |
| `source` | The source repository to check.                                                         | no | https://api.nuget.org/v3-flatcontainer/PACKAGE-NAME/index.json |
| `json-path` | The json path to extract the versions.                                               | no | $.versions[*] |
| `fail-when-not-found` | Will fail the job if the package of a specific version is not found. | no | false |
| `fail-when-found` | Will fail the job if the package of a specific version is found.         | no | false |

<div align="center">

---

## **⬅️ Action Output ➡️**
</div>

The name of the output is `result` and it returns a `boolean` of `true` or `false`.
Refer to the **Example** above for how to use the output of the action.

---

<h2 style="font-weight:bold;" align="center">🙏🏼 Contributing 🙏🏼</h2>

Interested in contributing? If so, click [here](https://github.com/KinsonDigital/.github/blob/master/docs/CONTRIBUTING.md) to learn how to contribute your time or [here](https://github.com/sponsors/KinsonDigital) if you are interested in contributing your funds via one-time or recurring donation.

<div align="center">

## **🔧 Maintainers 🔧**
</div>

  [![twitter-logo](https://raw.githubusercontent.com/KinsonDigital/.github/master/Images/twitter-logo-16x16.svg)Calvin Wilkinson](https://twitter.com/KDCoder) (KinsonDigital GitHub Organization - Owner)
  
  [![twitter-logo](https://raw.githubusercontent.com/KinsonDigital/.github/master/Images/twitter-logo-16x16.svg)Kristen Wilkinson](https://twitter.com/kswilky) (KinsonDigital GitHub Organization - Project Management, Documentation, Tester)
 
<br>

<h2 style="font-weight:bold;" align="center">🚔 Licensing And Governance 🚔</h2>

<div align="center">

[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg?style=flat)](https://github.com/KinsonDigital/.github/blob/master/docs/code_of_conduct.md)
[![MIT License](https://img.shields.io/github/license/kinsondigital/packagemonster)](https://github.com/KinsonDigital/PackageMonster/blob/master/LICENSE.md)
</div>

This software is distributed under the very permissive MIT license and all dependencies are distributed under MIT-compatible licenses.
This project has adopted the code of conduct defined by the **Contributor Covenant** to clarify expected behavior in our community.
 