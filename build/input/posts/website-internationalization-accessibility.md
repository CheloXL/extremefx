Title: Website Internationalization and Accessibility
Published: 2011-07-21
Tags: [Internationalization, Globalization, Accessibility, Web]
Lead: "The World Wide Web is capable of living up to its name: by being accessible to everyone in the world. This is more or less true, as there are still many web users whose needs aren’t adequately met. The Word Wide Web Consortium (W3C) has several recommendations on how to make a webpage accessible by users, not only with disabilities, but also by users that do not speak English."
---
The World Wide Web is capable of living up to its name: by being accessible to everyone in the world. This is more or less true, as there are still many web users whose needs aren&#8217;t adequately met. The Word Wide Web Consortium ([W3C](http://www.w3.org/)) has several recommendations on how to make a webpage accessible by users, not only with disabilities, but also by users that do not speak English.

### What is Accessibility?

Here is a definition from the Web Accessibility Initiative [WAI](https://www.w3.org/WAI/fundamentals/accessibility-intro/): &#8220;… Web accessibility means that people with disabilities can perceive, understand, navigate, and interact with the Web, and that they can contribute to the Web. Web accessibility also benefits others, including older people with changing abilities due to aging."

### Web Internationalization

Web Internationalization is the practice of making content available in a variety of languages, not simply one. On the web, languages determine the direction of the text, the fonts used, or even the dictionary for pronunciation used by a screen reader.

Basically, a website is composed of three layers: Content, Presentation and Behavior.

* The **content layer** is what your readers are coming to get when they come to your Web page. Content can consist of text or images and includes the pointers that your readers need to navigate around your website. In web development, **HTML** makes up the content layer and it also structures the web document.
* The **presentation layer** is how the document will look to your readers. This layer is defined by the **CSS** or styles that indicate how your document should be displayed and on what media types.
* The **behavior layer** is the layer of a webpage that performs a function. If you use Ajax or DHTML, it is the JavaScript that makes the page do something. If you have a PHP/.Net or any other kind of back-end, it is that back-end that produces results when your reader clicks something on the webpage. For most web pages, the first level of behavior is the JavaScript interactions on the page.

One of the principles of web accessibility is that these three types of information should be separated from one other. Blind users will certainly not be able to *see* anything on screen, so another way to *present* the information to that user needs to be created. Arabic online users would have an unpleasant experience trying to read left-aligned Arabic content and Chinese users would have a hard times trying to read 11px Chinese text. As you can see, accessibility it is not only for blind people.

### Accessibility Standards

You would have to spend a small fortune doing  website testing to assure proper presentation on all possible devices. You already know how hard it can be to fully test a design on mainstream browsers, if you have to accommodate really old browsers that do not follow the standards, like IE6. Adding assistive technology software and hardware to the mix makes the task nearly impossible.

So, how do web developers deal with these issues? Subject matter experts on accessibility around the world compiled their knowledge and created a set of recommendations for web developers to follow. By learning these standards, a web developer won&#8217;t have to spend time researching every possible access method. By following these standards, he can be reasonably sure that a compliant webpage will be accessible to a broad audience.

### Cascading Style Sheets

[Cascading Style Sheets (CSS)](https://www.w3.org/Style/CSS/learning) is a simple way to add style (fonts, colors, layout, etc.) to web pages. CSS were designed primarily to enable the separation of document content from document presentation.

If we broaden our understanding of *presentation* to mean something more like *representation*, thereby obviating even an implied visual form, it becomes possible to customize CSS for a range of output media. It also becomes possible to write separate style sheets for media, whose modes of presentation vary enormously, from Braille to printers to computer screens.

[CSS Level 2](http://www.w3.org/TR/CSS21/) media types were defined in 1998. Their details (as excerpted from the W3C document focusing on accessibility-specific media) are as follows:

* **Aural**: Intended for speech synthesizers.
* **Braille**: Intended for Braille tactile feedback devices.
* **Embossed**: Intended for paged Braille printers.
* **Print**: Intended for paged, opaque material and for documents viewed on screen in print preview mode.
* **Screen**: Intended primarily for color computer screens.
* **TTY**: Intended for media using a fixed-pitch character grid, such as teletypes, terminals, or portable devices with limited display capabilities.

The preferred method of styling a document involves external style sheets &#8211; separately-maintained documents that can be included by reference in an unlimited number of HTML files and updated in one single operation. Style sheets are then linked to a document as follow: `<link rel="stylesheet" href="stylesheet.css" type="text/css" />`.

To use media style sheets, simply add one more attribute to the link attribute *media*. Some possible examples:

* `<link rel="stylesheet" href="tty.css" type="text/css" media="tty" />`
* `<link rel="stylesheet" href="aural.css" type="text/css" media="aural" />`
* `<link rel="stylesheet" href="braille.css" type="text/css" media="braille" />`

If you do not specify a medium for a style sheet, graphical Web browsers default to an interpretation of `media="all"`. In CSS Level 2, you do not have to use separate style sheets for different media; you can use the `@media` rule to specify the media.

You can also pair media queries with pseudo-classes, most notability the *lang* pseudo-class. The CSS Level 2 specification defines a special pseudo class, `:lang()`, for indicating rules that should be applied only to elements that match a certain language. Such a rule is written as follows: `:lang(zh-CN) { font-size: 120%; }`.

This would display anything written in *Simplified Chinese language* with a font size 1.2 times larger than the base size specified for the whole document.

Under the [Web Content Accessibility Guidelines](http://www.w3.org/TR/WCAG20/), you are required to specify changes in the language used in documents. You do this by adding the `lang="languagecode"` attribute to virtually any tag (like `<p></p>`, `<span></span>`, `<cite></cite>`, or `<hx></hx>`. Also, in order to specify a change in language, you must already have declared the default, base, or original language, which you do by adding `lang="languagecode"` to the `<body>` or (preferably) `<html>` tags.

```html
<p lang="zh-CN">关于我们</p>
```

By itself, `:lang()` is not particularly useful, but when combined with other CSS rules and properties, it can be quite powerful.

#### List Markers

One way in which `:lang()` rules can be used is to set an appropriate marker for ordered lists. For example: `ol li:lang(hy-AM) { list-style-type: armenian; }`.

That will set the list style to traditional uppercase Armenian numbering for Armenian text. There are specific values for Armenian, Georgian, Roman and Greek numbers/letters. You can find a complete list at [W3C](https://www.w3.org/TR/CSS21/generate.html#list-style) Generated content, automatic numbering, and lists.

#### Bidirectional Text
Two CSS properties, direction and unicode-bidi, are used to affect the calculation of the correct direction. In most cases, you won&#8217;t need to use these properties if you set the correct language on the document/text run, but you may occasionally need to change text direction. You should first use the unicode-bidi property to create an additional level of embedding or to set up an override. Then the value of direction can be set to either ltr (left-to-right) or rtl (right-to-left).

### Summary

Users with disabilities are as entitled to use the Web as anyone else, but often they are unable to access websites due to careless web design. Using Cascading Style Sheets is an excellent first step toward developing a website that can be used by everyone, as style sheets separate presentation from content. [Assistive technology devices](https://www.microsoft.com/en-us/accessibility/) and software can often enable access by disabled users, but only if sites are designed in accordance with Web accessibility standards. The W3C has produced Web Content Accessibility Guidelines that are an invaluable resource for Web developers.

In addition to users with disabilities, users in non-English-speaking countries also use the Web. CSS is designed with internationalization in mind; for example, rules can be made for specific languages with the `:lang()` pseudo-element, and the list-style-type property can produce a number of non-English number markers.

#### Keeping the goal of website globalization in mind

Whether you are trying to release a multilingual product in order to increase your global market share and ROI or you are trying to increase your company&#8217;s global operational efficiencies by developing multilingual websites, website globalization is a requirement to make either a reality. Each client&#8217;s needs are somewhat unique and there are a variety of factors that can influence resources and costs involved in a complex software globalization project.
