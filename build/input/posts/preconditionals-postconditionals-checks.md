Title: Preconditionals and postconditionals checks
Published: 2009-03-31
Tags: [Flash, Code, Example]
Lead: How many times you found yourself writing preconditions before executing some code (like argument validation) and post-conditions (validating some result before returning the value/using on another function)?
---
How many times you found yourself writing preconditions before executing some code (like argument validation) and post-conditions (validating some result before returning the value/using on another function)?

So, for example:

```actionscript
if (arg == null) throw new ArgumentError("arg can not be null");
if (!(arg is IInterface)) throw new ArgumentError("arg must be of type IInterface");
....
if (ret == null) throw new Error("Something went wrong!");
return ret;
```

That's good practice, but when you have to validate several conditions the code starts to look messy. For that reason, I created a couple of helper classes that takes cares of the job, providing a nice fluent typing.

## Pre-conditional checking examples:

```actionscript
requiresNumber(pIndex, "pIndex").isLessThan(_size);
requires(pCollection, "pCollection").isNotNull();
requiresString(pKey, "key").isNotNullOrEmpty();
requires(pValue, "value").isNotNull();
requires(pNode._parent, "node").isNotSameAs(this, "You cannot attach a node to itself").isTypeOf(TreeNode);
```

## Post-conditional checking examples:

```actionscript
ensuresBool(ExternalInterface.available, "ExternalInterface is not available.").isTrue();
ensures(_entries[pOldKey]).isNotNull("Key ["+pOldKey+"] does not exists.");
ensuresNumber(days).isLessOrEqualThan(10675199, "Invalid number of days: "+days);
```

### Usage
You have 6 specific conditional checks (Array, Boolean, Date, Number, String) and one general. You access the conditionals by using either the requires[Type] functions or ensures[Type] functions.

The *requires* functions requires at least one parameter: the value you want to check. You can also pass a second parameter that represents the *name* of that value (you usually use *requires* to validate method arguments).

The conditions may have different number of parameters (depending on what are you checking). The last one is an additional string you may want to attach to the error description.

The *ensures* functions are similar to *requires*. Ensures needs at least one parameter (the value). The second argument is an additional message you may want to attach to the complete validation process, and the third one is the *name* of the variable you are checking.

This library is part of my flash framework. You can download it from [Google Code](http://code.google.com/p/efxflashsource/).

**Note:** There is a bug on the FlexSDK4 that does not let me compile the full library as a .swc, nor create the documentation (If you are interested: [BUG](http://bugs.adobe.com/jira/browse/SDK-20251)).
You have to download the library from the SVN repository. I will post a new ZIP file when the bug gets fixed.
