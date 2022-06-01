<h1 align="center">

**Got Nuget?🍫**
</h1>

<div align="center">

### GitHub Action for checking if a NuGet package exists in the public NuGet gallery package repository [nuget.org](https://www.nuget.org)

<div hidden>TODO: ADD BADGES HERE</div>

</div>


<div align="center">

## **What is it?**
</div>


This **GitHub Action** can be used to verify if a NuGet package of a particular version exists.


<div align="center"><h3 style="font-weight:bold">Quick Example</h3></div>


```yaml
name: GotNuget Action Sample

on:
  workflow_dispatch:

jobs:
  Test_Action:
    name: Test GotNuget GitHub Action
    runs-on: ubuntu-latest 👈🏼 # Must be ubuntu
    steps:
    - uses: actions/checkout@v2

    - name: Check If Nuget Package Exists
      id: nuget-exists
      uses: KinsonDigital/GotNuget@v1.0.0-preview.1
      with:
        package-name: MyPackage
        version: 1.2.3

    - name: Print Output Result #Powershell Core
      shell: pwsh 👈🏼 # Must be explicit with the shell to use powershell on ubuntu
      run: |
        #        Output name for the GotNuget github action 👇🏼
        #                                               _____|_____
        #                                              |          |
        $nugetExists = "${{ steps.nuget-exists.outputs.nuget-exists }}";
        
        if ($nugetExists -eq "true") {
          Write-Host "The nuget package exists!!";
        } else {
          Write-Host "The nuget package does not exist!!";
        }
```

<div align="left">
<a href="#examples">More Examples Below!! 👇🏼</a>
</div>

---

<div align="center"><h2 style="font-weight:bold">What does it do?</h2></div>

It is simple.  It simply goes out to [nuget.org](https://www.nuget.org) and checks to see if a nuget package of a particular version exists.  If it does, it returns and output value of `"true"`, if not, then it returns `"false"`.
Thats it!!

---

<div align="center">

## **Action Inputs**
</div>

TODO: Show action inputs in table

| Input Name | Description | Required | Default Value |
|---|:----|:---:|:---:|
| `package-name` | The name of the nuget package. | yes | N/A |
| `version` | The version of the package to look for. | yes | N/A |
| `fail-when-not-found` | Will fail the job if the nuget package of a specific version is not found. | no | false |

---

<div align="center" style="font-weight:bold">

## **Examples**
</div>

<div align="center">

### **Fail the job if the package is not found**
</div>

``` yaml
- name: Check If Nuget Package Exists
  uses: KinsonDigital/GotNuget@v1.0.0-preview.1
  with:
    package-name: MyPackage
    version: 100.20.3
    fail-when-not-found: true
```

---

<div align="center">

## **Other Info**
</div>

<div align="left">

### License
- [MIT License - GotNuget](https://github.com/KinsonDigital/GotNuget/blob/preview/v1.0.0-preview.1/LICENSE)
</div>

<div align="left">

### Maintainer
</div>

- [Calvin Wilkinson](https://github.com/CalvinWilkinson) (Owner and main contributor of the GitHub organization [KinsonDigital](https://github.com/KinsonDigital))
  - [Got Nuget](https://github.com/KinsonDigital/GotNuget) is used in various projects for this organization with great success.
- Click [here](https://github.com/KinsonDigital/GotNuget/issues/new/choose) to report any issues for this GitHub action!!

<div align="right">
<a href="#what-is-it">Back to the top!👆🏼</a>
</div>
