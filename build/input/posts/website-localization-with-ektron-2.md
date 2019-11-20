Title: Website Localization with Ektron – Part 2
Published: 2010-08-27
Tags: Localization, Translation, Cms
Lead: Setting up correct Locales, adding Lang attributes and localizing hard-coded images are among the technical steps detailed in this final blog in this 2-part series on Website Localization with Ektron CMS.
---
This is the Part 2 on *Website Localization using Ektron Web CMS*. In this part I will discuss how to correctly setup a multilingual website within Ektron and also provide some additional information about website localization in general.

### Setting up Ektron

When setting up a multilingual website in [Ektron](http://www.ektron.com/), there are several factors that fall outside the translation-only process that is built into the web system. Let’s first set-up Ektron through the WorkArea.

![Ektron Setup](/assets/images/Ektron-Setup.png)

You should first go to the configuration screen, and under *Language Settings* you should enable the languages you want your website to be translated into. You enable a language by checking the boxes that are under the green check mark icon.

It is worth noting that while I’m using the word *language*, what we are really selecting is a [locale](http://en.wikipedia.org/wiki/Locale). A locale is the association of a language and a country. Locale settings are important due to, for example, the *English* language using different words that represent the same meaning in the US and in UK. The same applies to currency, measurements ([metric system](http://en.wikipedia.org/wiki/Metric_system)/imperial units/customary units/etc.).

### Preparing the templates for your multilingual website

While most of the translation workflow process will be handled by Ektron, the templates you have created ([aspx](http://en.wikipedia.org/wiki/Aspx) pages) to display the content on your website will also require preparation to support multilingual content.

#### Important tips about creating Ektron templates for website localization

1. **Content translation expansion:** Make sure there is enough room for the text to accommodate possible expansion once translated (usually text length could expand up to a 25% of the original), or that the web template was coded in a way that expands or shrinks based on the displayed content.
2. **Hard-coded text:** text that it is hardcoded into the web template will not be exported for translation. Having different web templates per language sometimes is not possible. This means that every time you have to make one change to the template you need to apply the same change over and over again for each language. To solve this issue, I would recommend:
   * Have all hard-coded text extracted into a resource file (that needs to be sent for translation also), or
   * Create a *Dictionary* folder within the CMS, setup a simple **SmartForm/XSLT** that will hold/display the text and then replace all the hardcoded text on the web templates with Content controls that will have their IDs pointing to each entry on the Dictionary.

![Ektron Dictionary](/assets/images/Ektron-Dictionary.png)

The second option has the advantage of letting the Website administrator edit the website text, and also, since the text now is part of the CMS content, it can be exported/imported back by the Website admin.

1. **Localization and images/PDFs/documents**: These items are not exported for translation. If the images you used on the website do not have any text, this is not a problem. But [PDFs](http://en.wikipedia.org/wiki/Pdf) or other documents need to be sent separately to the translation company if you need them translated.
2. **Text direction:** Ektron handles Left-to-Right (LTR) and Right-to-Left (RTL) text without problems. But you have to tell the browser how you want it to display the text on screen. If you are not translating your website into Arabic/Hebrew/Persian/etc., this is not an issue. But if you are localizing your website into RTL direction for languages like Arabic, using Ektron, you will have to employ some code on the backend that detects the language direction and writes the appropriate `dir` attribute on the html page (`dir="rtl"`). Also, you may want to add this attribute to a container inside the html page and not to the page per-se. In some browsers, applying a right-to-left direction in the html or body tag will affect the user interface too. If the page has a scroll bar, it will appear on the left side of the window. JavaScript alert boxes may also be mirror imaged. Some speakers of languages that use right-to-left scripts prefer the directionality of the user interface to be associated with the desktop environment, not with the content of a particular document. Because of this, it may be preferable to not declare the document directionality on the html or body tag.
3. **Localization and character encoding:** If a browser is unable to detect the character encoding used in a Web document, the user may be presented with unreadable text. Declaring the character encoding of the web template is important for anyone producing multilingual websites. A Unicode encoding can support many languages and can accommodate pages and forms in any mixture of those languages. Its use also eliminates the need for server-side logic to individually determine the character encoding for each page served or each incoming form submission. This significantly reduces the complexity of dealing with a multilingual site or application. A Unicode encoding also allows many more languages to be mixed on a single page than almost any other choice. Authors are encouraged to use [UTF-8](http://www.utf8.com/) as the default character encoding when creating templates and use the appropriate meta-tag on each template (`<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>`)
4. **Page language:** Declaring the default language is already important for web applications such as [accessibility](http://www.w3.org/TR/WCAG10/#gl-abbreviated-and-foreign) and search engine optimization (SEO), but many [other possible applications](http://www.w3.org/International/questions/qa-lang-why) for this information may emerge over time. It is usually very easy to do when creating the content, but more difficult to retrofit later in order to take advantage of language-related features. The easiest way is to declare the language on the HTML tag of your web template (`<html lang="en" xml:lang="en">`). It can be overridden for portions of the document as required.

### Exporting and translating content in Ektron

![Ektron Export](/assets/images/Ektron-Export.png)

Once you have your website ready for translation, you can export the content of the website with a click of your mouse. You select the root folder and from the **"Action"** menu, select **"Export for translation"**. Ektron will go through each page on your website, create an XLIFF file ready for translation and compress it with all the other pages in one single ZIP file (per language) that you can download and send for translation.

Each ZIP file will have a complete copy of your website ready for translation. One feature Ektron has is that you don’t need to track which pages you modified/added into the website after the content was translated. Ektron will do that for you and the next time you export the content, only the new pages/changed content will be exported, saving you time and money.

The content is then translated by a localization services agency and sent back to you, again, as ZIP files.

### Importing the translated content back into the Ektron CMS

![Ektron Import](/assets/images/Ektron-Import.png)

Once the content has been translated, you need to import back the translated content into the Ektron CMS. As with exporting, this operation is as easy as going to the settings and selecting the **"Import XLIFF Files"** option. After you browsed your machine for the zipped XLIFF files and clicked on the upload button, Ektron will insert back each translated contents under the page/language they belong.

If you have setup a content approval chain, the newly imported content will be placed as "Checked In" for your web reviewers and/or QA testers to review and approve.

At this point, your multilingual website is ready. I would always highly recommend running a standard website Quality Assurance process of the site in case any issues surface (i.e., some text may need some adjustments, still having hard-coded text on the templates, etc).

I hope this article was useful and serves as a starting point to prepare an Ektron based website to support multiple-languages. In my next article I will show you how to create a series of simple controls that you can drop in your templates in order to take care of some common issues with localization.

