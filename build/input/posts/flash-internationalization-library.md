Title: Flash internationalization library
Published: 2008-04-21
Tags: [Flash, Internationalization, Translation, Code]
Lead: This library will help you to internationalize your flash application. It supports localization of numbers, date/time, percentages and strings. It can also parse back numbers and date/time formats on a specific locale into flash internal …
---
This library will help you to internationalize your flash application. It supports localization of numbers, date/time, percentages and strings. It can also parse back numbers and date/time formats on a specific locale into flash internal formats.

### Details

Most of the code is well documented. I choose to follow the .net c# conventions for formatting as is the one I use the most.

There is also a resource management class that will let you manage language-specific resources, like strings and bitmaps. The resource manager uses the standard XLIFF format and it supports trans-unit nodes as well as bin-unit nodes.

Bin-unit nodes can have external or internal content. External content can be anything that flash supports natively like text files, xml, sound, video and images. External content will be loaded at runtime after the XLIFF file is parsed.

Internal content can only be images.

The library also supports text pluralization. For the library to know a string is a pluralized string, the string “[I18N]” must precede the list of pluralization's.

ex: `[I18N]|{0} file deleted.|{0} files deleted.`

The number of plurals will depend on the language. Most languages uses two forms, singular used for one only, but other languages have more that one plural form (and others, like some of the Asian family, have no distinction between the singular and plural form).

For example, Polish uses e.g. plik (file) this way:

* 0 plików
* 1 plik
* 2-4 pliki
* 5-21 plików
* 22-24 pliki
* 25-31 plików
* etc.

And so on. Plural forms must be sorted like in real life. So, the singular first (if exists) and then the plurals.

The source code has a demo application that uses all the features the framework has. Please check that demo and the documentation for more information.

#### TODO / Missing

* *Calendars:* Gregorian calendar and all its derivated works (Japanese, Julian, Korean, Taiwan, ThaiBuddhist). All the Lunar Calendars are not implemented. Also, the UmAlQuraCalendar it's not implemented at all.
* *HijriCalendar:* Again, I implemented the algorithm, but I'm not sure if it works correctly. Need to check how the addHijriDate parameter works, if this parameter is the same as hijriAdjustment in .net, and if the calendar works at all.
* Native digits on the NumberFormatInfo is implemented but currently the system does not replace digits on the output string. Need to check with someone that knows Arabic on how this works.
* Collation tables (tables used for correct string sorting based on the language sorting rules) are not implemented. Not sure if I will be able to implement this feature, as they are pretty big. The collation table for the Japanese language only is a 9mb binary file.
* Bi-Di algorithm (have a rought version working, need to test against all the test cases).
* Inline node parsing on XLIFF files (to correctly handle inline tags, like bold, italic, links, etc).

Source code and downloads at: <a href="http://code.google.com/p/efxflashsource">GoogleCode</a>
