Title: Downgrading a Nuget package
Published: 2013-10-17
Tags: [CSharp, Code]
Lead: "For my own reference: How to downgrade a nuget package after a previous upgrade broke my VS solution. Instructions inside :)"
---
Today I had a problem where I had blindly upgraded EF to version 6 across an entire solution, only to find out that the version was incompatible with the current MySql provider.

Due to time constraints, instead of trying to revert back the changes from the repository/merge with the other work already in progress to fix the problems,
I decided to roll back EF to the previous version. So, I started to mess out with the nuget package manager console.

First I needed to get a list of projects that have a reference to the nuget package:

```
$projects = Get-Project -All | Select @{ Name="ProjectName";Expression={$_.ProjectName}}, @{Name="Has";Expression={Get-Package EntityFramework -Project $_.Name}}
```

Then, I uninstalled the package:

```
projects | select { Uninstall-Package EntityFramework -ProjectName $_.ProjectName -Force }
```

It's worth noting the use of the *Force* switch here. That can be quite dangerous as it can break other packages
(see <a href="http://docs.nuget.org/docs/reference/package-manager-console-powershell-reference#Uninstall-Package">NuGet Docs</a>)</p>

*Finally, installed the new one:*

```
$projects | select { Install-Package EntityFramework -Version 5.0.0 -ProjectName $_.ProjectName }
```

*And that's it. That solved my problem and now I can continue working where I left...*
