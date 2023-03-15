<h1 align="center">

**Got Nuget?🍫**
</h1>

<div align="center">

<div hidden>TODO: ADD BADGES HERE</div>

</div>


<div align="center">

## **What is it?**
</div>

### Checks if a NuGet package with a particular name and version exists in the public NuGet gallery package repository [nuget.org](https://www.nuget.org).

<div align="center"><h2 style="font-weight:bold">⚠️Quick Note⚠️</h2></div>

This GitHub action is built using C#/NET and runs in a docker container.  If the job step for running this action is contained in a job that runs on **Windows**, you will need to move the step to a job that runs on **Ubuntu**.  You can split up your jobs to fulfill `runs-on` requirements of the GitHub action. This can be accomplished by moving the step into it's own job.  You can then route the action step outputs to the job outputs and use them throughout the rest of your workflow. For more information, refer to the Github documentation links below:

For more info on step and job outputs, refer to the GitHub documentation links below:
- [Defining outputs for jobs](https://docs.github.com/en/actions/using-jobs/defining-outputs-for-jobs)
- [Setting a step action output parameter](https://docs.github.com/en/actions/using-workflows/workflow-commands-for-github-actions#setting-an-output-parameter)

This **GitHub Action** can be used to verify whether or not a particular version of a NuGet package exists.


<div align="center"><h2 style="font-weight:bold">Quick Example</h2></div>


```yaml
name: GotNuget Action Sample

on:
  workflow_dispatch:

jobs:
  Test_Action:
    name: Test GotNuget GitHub Action
    runs-on: ubuntu-latest 👈🏼 # Must be this value
    steps:
    - uses: actions/checkout@v3

    - name: Check If Nuget Package Exists
      id: nuget-exists
      uses: KinsonDigital/GotNuget@v1.0.0-preview.1
      with:
        package-name: MyPackage 👈🏻 # Required input
        version: 1.2.3 👈🏻 # Required input
        fail-when-not-found: true 👈🏻 # Optional input

    - name: Print Output Result #PowerShell Core
      shell: pwsh 👈🏼 # Must be explicit with the shell to use PowerShell on Ubuntu
      run: |
        #        Output name for the GotNuget GitHub action 👇🏼
        #                                               _____|_____
        #                                              |          |
        $nugetExists = "${{ steps.nuget-exists.outputs.nuget-exists }}";
        
        if ($nugetExists -eq "true") {
          Write-Host "The NuGet package exists!!";
        } else {
          Write-Host "The NuGet package does not exist!!";
        }
```

<div align="left">
<a href="#examples">More Examples Below!! 👇🏼</a>
</div>

---

<div align="center"><h2 style="font-weight:bold">What does it do?</h2></div>

It is simple!  It goes out to [nuget.org](https://www.nuget.org) and checks to see if a NuGet package of a particular version exists.  If it does, it returns and output value of `"true"`, if not, then it returns `"false"`.
Thats it!!

---

<div align="center">

## **Action Inputs**
</div>

TODO: Show action inputs in table

| Input Name | Description                                                                | Required | Default Value |
|---|:---------------------------------------------------------------------------|:---:|:---:|
| `package-name` | The name of the NuGet package.                                             | yes | N/A |
| `version` | The version of the package.                                                | yes | N/A |
| `fail-when-not-found` | Will fail the job if the NuGet package of a specific version is not found. | no | false |

<div align="center">

## **Action Output**
</div>

The name of the output is `nuget-exists` and it returns a `boolean` of `true` or `false`.
Refer to the _**Quick Example**_ for how to use the output of the action.

---

<div align="center">

## **🔧Maintainers**
</div>

- [![twitter-logo](https://raw.githubusercontent.com/KinsonDigital/.github/master/Images/twitter-logo-16x16.svg)Calvin Wilkinson](https://twitter.com/KDCoder) (KinsonDigital GitHub Organization - Owner)
- [![twitter-logo](https://raw.githubusercontent.com/KinsonDigital/.github/master/Images/twitter-logo-16x16.svg)Kristen Wilkinson](https://twitter.com/kswilky) (KinsonDigital GitHub Organization - Documentation Maintainer & Tester)
 

<div align="center">

## **Other Info**

</div>

- Click [here](https://github.com/KinsonDigital/GotNuget/issues/new/choose) to report any issues for this GitHub action!!
- [MIT License](https://github.com/KinsonDigital/GotNuget/blob/preview/v1.0.0-preview.1/LICENSE)


<div align="right">
<a href="#what-is-it">Back to the top!👆🏼</a>
</div>
