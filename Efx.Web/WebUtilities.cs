// Decompiled with JetBrains decompiler
// Type: Efx.Web.WebUtilities
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Efx.Web
{
  public static class WebUtilities
  {
    private static readonly Regex _aReplaceTag;
    private static readonly Regex _amputator;
    private static readonly Regex _attrFilter;
    private static readonly Regex _attrFilterImg;
    private static readonly Regex _attrFilterLink;
    private static readonly Regex _attrFilterReplace;
    private static readonly string[] _blockTags;
    private static readonly Dictionary<string, string> _charReplacements;
    private static readonly Regex _compact;
    private static readonly Regex _entities1;
    private static readonly Regex _entities2;
    private static readonly string[] _expanded;
    private static readonly bool _fullTrust;
    private static readonly Regex _imgReplaceTag;
    private static readonly string[] _inChars;
    private static readonly List<Regex> _neverAllowedRegex;
    private static readonly Dictionary<string, string> _neverAllowedStr;
    private static readonly List<Regex> _nonDisplayables;
    private static readonly string[] _outChars;
    private static readonly Regex _removeHandlers;
    private static readonly Regex _removeNaughtyHtml;
    private static readonly Regex _removeNaughtyScript;
    private static readonly Regex _scriptReplaceTag;
    private static readonly Regex _semicolon;
    private static readonly Regex _url;
    private static readonly Regex _utf16;
    private static readonly string _xssHash;

    static WebUtilities()
    {
      // ISSUE: unable to decompile the method.
    }

    public static bool IsFullTrust
    {
      get
      {
        return WebUtilities._fullTrust;
      }
    }

    public static string EntitiesToUtf(this string pText, bool pInputIsXml = false)
    {
      int startIndex1 = 0;
      while (true)
      {
        int startIndex2;
        string index;
        do
        {
          startIndex2 = pText.IndexOf('&', startIndex1);
          if (startIndex2 != -1)
          {
            int num = pText.IndexOf(';', startIndex2, Math.Min(64, pText.Length - startIndex2));
            if (num != -1)
            {
              startIndex1 = startIndex2 + 1;
              index = pText.Substring(startIndex2, num - startIndex2 + 1);
            }
            else
              goto label_2;
          }
          else
            goto label_6;
        }
        while (!WebUtilities._charReplacements.ContainsKey(index));
        goto label_4;
label_2:
        startIndex1 = startIndex2 + 1;
        continue;
label_4:
        pText = pText.Replace(index, WebUtilities._charReplacements[index]);
      }
label_6:
      if (!pInputIsXml)
        return pText.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'");
      return pText;
    }

    public static string FixBadlyEncodedAmpersands(this string pText)
    {
      return WebUtilities._amputator.Replace(pText, "&amp;");
    }

    public static CompressionMethod GetCompressionMethod(HttpRequest pRequest)
    {
      string header = pRequest.Headers["Accept-Encoding"];
      if (string.IsNullOrEmpty(header))
        return CompressionMethod.None;
      HttpBrowserCapabilities browser = pRequest.Browser;
      if (browser.Browser == "IE" && browser.MajorVersion < 7)
        return CompressionMethod.None;
      return WebUtilities.GetFilterForScheme((IEnumerable<string>) header.Split(','));
    }

    public static string GetServerBaseUri(HttpRequest pRequest)
    {
      if (pRequest == null)
        return string.Empty;
      return (pRequest.Url.Scheme + "://" + pRequest.Url.Authority + pRequest.ApplicationPath).TrimEnd('/') + "/";
    }

    public static string GetServerBaseUri()
    {
      return WebUtilities.GetServerBaseUri(HttpContext.Current.Request);
    }

    public static string MimeType(string fileName)
    {
      switch (Path.GetExtension(fileName).ToLowerInvariant())
      {
        case ".3dm":
          return "x-world/x-3dmf";
        case ".3dmf":
          return "x-world/x-3dmf";
        case ".a":
          return "application/octet-stream";
        case ".aab":
          return "application/x-authorware-bin";
        case ".aam":
          return "application/x-authorware-map";
        case ".aas":
          return "application/x-authorware-seg";
        case ".abc":
          return "text/vnd.abc";
        case ".acgi":
          return "text/html";
        case ".afl":
          return "video/animaflex";
        case ".ai":
          return "application/postscript";
        case ".aif":
          return "audio/aiff";
        case ".aifc":
          return "audio/aiff";
        case ".aiff":
          return "audio/aiff";
        case ".aim":
          return "application/x-aim";
        case ".aip":
          return "text/x-audiosoft-intra";
        case ".ani":
          return "application/x-navi-animation";
        case ".aos":
          return "application/x-nokia-9000-communicator-add-on-software";
        case ".aps":
          return "application/mime";
        case ".arc":
          return "application/octet-stream";
        case ".arj":
          return "application/arj";
        case ".art":
          return "image/x-jg";
        case ".asf":
          return "video/x-ms-asf";
        case ".asm":
          return "text/x-asm";
        case ".asp":
          return "text/asp";
        case ".asx":
          return "video/x-ms-asf";
        case ".au":
          return "audio/basic";
        case ".avi":
          return "video/avi";
        case ".avs":
          return "video/avs-video";
        case ".bcpio":
          return "application/x-bcpio";
        case ".bin":
          return "application/octet-stream";
        case ".bm":
          return "image/bmp";
        case ".bmp":
          return "image/bmp";
        case ".boo":
          return "application/book";
        case ".book":
          return "application/book";
        case ".boz":
          return "application/x-bzip2";
        case ".bsh":
          return "application/x-bsh";
        case ".bz":
          return "application/x-bzip";
        case ".bz2":
          return "application/x-bzip2";
        case ".c":
          return "text/plain";
        case ".c++":
          return "text/plain";
        case ".cat":
          return "application/vnd.ms-pki.seccat";
        case ".cc":
          return "text/plain";
        case ".ccad":
          return "application/clariscad";
        case ".cco":
          return "application/x-cocoa";
        case ".cdf":
          return "application/cdf";
        case ".cer":
          return "application/pkix-cert";
        case ".cha":
          return "application/x-chat";
        case ".chat":
          return "application/x-chat";
        case ".class":
          return "application/java";
        case ".com":
          return "application/octet-stream";
        case ".conf":
          return "text/plain";
        case ".cpio":
          return "application/x-cpio";
        case ".cpp":
          return "text/x-c";
        case ".cpt":
          return "application/x-cpt";
        case ".crl":
          return "application/pkcs-crl";
        case ".crt":
          return "application/pkix-cert";
        case ".csh":
          return "application/x-csh";
        case ".css":
          return "text/css";
        case ".cxx":
          return "text/plain";
        case ".dcr":
          return "application/x-director";
        case ".deepv":
          return "application/x-deepv";
        case ".def":
          return "text/plain";
        case ".der":
          return "application/x-x509-ca-cert";
        case ".dif":
          return "video/x-dv";
        case ".dir":
          return "application/x-director";
        case ".dl":
          return "video/dl";
        case ".doc":
          return "application/msword";
        case ".dot":
          return "application/msword";
        case ".dp":
          return "application/commonground";
        case ".drw":
          return "application/drafting";
        case ".dump":
          return "application/octet-stream";
        case ".dv":
          return "video/x-dv";
        case ".dvi":
          return "application/x-dvi";
        case ".dwf":
          return "model/vnd.dwf";
        case ".dwg":
          return "image/vnd.dwg";
        case ".dxf":
          return "image/vnd.dwg";
        case ".dxr":
          return "application/x-director";
        case ".el":
          return "text/x-script.elisp";
        case ".elc":
          return "application/x-elc";
        case ".env":
          return "application/x-envoy";
        case ".eps":
          return "application/postscript";
        case ".es":
          return "application/x-esrehber";
        case ".etx":
          return "text/x-setext";
        case ".evy":
          return "application/envoy";
        case ".exe":
          return "application/octet-stream";
        case ".f":
          return "text/plain";
        case ".f77":
          return "text/x-fortran";
        case ".f90":
          return "text/plain";
        case ".fdf":
          return "application/vnd.fdf";
        case ".fif":
          return "image/fif";
        case ".fli":
          return "video/fli";
        case ".flo":
          return "image/florian";
        case ".flx":
          return "text/vnd.fmi.flexstor";
        case ".fmf":
          return "video/x-atomic3d-feature";
        case ".for":
          return "text/x-fortran";
        case ".fpx":
          return "image/vnd.fpx";
        case ".frl":
          return "application/freeloader";
        case ".funk":
          return "audio/make";
        case ".g":
          return "text/plain";
        case ".g3":
          return "image/g3fax";
        case ".gif":
          return "image/gif";
        case ".gl":
          return "video/gl";
        case ".gsd":
          return "audio/x-gsm";
        case ".gsm":
          return "audio/x-gsm";
        case ".gsp":
          return "application/x-gsp";
        case ".gss":
          return "application/x-gss";
        case ".gtar":
          return "application/x-gtar";
        case ".gz":
          return "application/x-gzip";
        case ".gzip":
          return "application/x-gzip";
        case ".h":
          return "text/plain";
        case ".hdf":
          return "application/x-hdf";
        case ".help":
          return "application/x-helpfile";
        case ".hgl":
          return "application/vnd.hp-hpgl";
        case ".hh":
          return "text/plain";
        case ".hlb":
          return "text/x-script";
        case ".hlp":
          return "application/hlp";
        case ".hpg":
          return "application/vnd.hp-hpgl";
        case ".hpgl":
          return "application/vnd.hp-hpgl";
        case ".hqx":
          return "application/binhex";
        case ".hta":
          return "application/hta";
        case ".htc":
          return "text/x-component";
        case ".htm":
          return "text/html";
        case ".html":
          return "text/html";
        case ".htmls":
          return "text/html";
        case ".htt":
          return "text/webviewhtml";
        case ".htx":
          return "text/html";
        case ".ice":
          return "x-conference/x-cooltalk";
        case ".ico":
          return "image/x-icon";
        case ".idc":
          return "text/plain";
        case ".ief":
          return "image/ief";
        case ".iefs":
          return "image/ief";
        case ".iges":
          return "application/iges";
        case ".igs":
          return "application/iges";
        case ".ima":
          return "application/x-ima";
        case ".imap":
          return "application/x-httpd-imap";
        case ".inf":
          return "application/inf";
        case ".ins":
          return "application/x-internett-signup";
        case ".ip":
          return "application/x-ip2";
        case ".isu":
          return "video/x-isvideo";
        case ".it":
          return "audio/it";
        case ".iv":
          return "application/x-inventor";
        case ".ivr":
          return "i-world/i-vrml";
        case ".ivy":
          return "application/x-livescreen";
        case ".jam":
          return "audio/x-jam";
        case ".jav":
          return "text/plain";
        case ".java":
          return "text/plain";
        case ".jcm":
          return "application/x-java-commerce";
        case ".jfif":
          return "image/jpeg";
        case ".jfif-tbnl":
          return "image/jpeg";
        case ".jpe":
          return "image/jpeg";
        case ".jpeg":
          return "image/jpeg";
        case ".jpg":
          return "image/jpeg";
        case ".jps":
          return "image/x-jps";
        case ".js":
          return "application/x-javascript";
        case ".jut":
          return "image/jutvision";
        case ".kar":
          return "audio/midi";
        case ".ksh":
          return "application/x-ksh";
        case ".la":
          return "audio/nspaudio";
        case ".lam":
          return "audio/x-liveaudio";
        case ".latex":
          return "application/x-latex";
        case ".lha":
          return "application/octet-stream";
        case ".lhx":
          return "application/octet-stream";
        case ".list":
          return "text/plain";
        case ".lma":
          return "audio/nspaudio";
        case ".log":
          return "text/plain";
        case ".lsp":
          return "application/x-lisp";
        case ".lst":
          return "text/plain";
        case ".lsx":
          return "text/x-la-asf";
        case ".ltx":
          return "application/x-latex";
        case ".lzh":
          return "application/octet-stream";
        case ".lzx":
          return "application/octet-stream";
        case ".m":
          return "text/plain";
        case ".m1v":
          return "video/mpeg";
        case ".m2a":
          return "audio/mpeg";
        case ".m2v":
          return "video/mpeg";
        case ".m3u":
          return "audio/x-mpequrl";
        case ".man":
          return "application/x-troff-man";
        case ".map":
          return "application/x-navimap";
        case ".mar":
          return "text/plain";
        case ".mbd":
          return "application/mbedlet";
        case ".mc$":
          return "application/x-magic-cap-package-1.0";
        case ".mcd":
          return "application/mcad";
        case ".mcf":
          return "text/mcf";
        case ".mcp":
          return "application/netmc";
        case ".me":
          return "application/x-troff-me";
        case ".mht":
          return "message/rfc822";
        case ".mhtml":
          return "message/rfc822";
        case ".mid":
          return "audio/midi";
        case ".midi":
          return "audio/midi";
        case ".mif":
          return "application/x-mif";
        case ".mime":
          return "message/rfc822";
        case ".mjf":
          return "audio/x-vnd.audioexplosion.mjuicemediafile";
        case ".mjpg":
          return "video/x-motion-jpeg";
        case ".mm":
          return "application/base64";
        case ".mme":
          return "application/base64";
        case ".mod":
          return "audio/mod";
        case ".moov":
          return "video/quicktime";
        case ".mov":
          return "video/quicktime";
        case ".movie":
          return "video/x-sgi-movie";
        case ".mp2":
          return "audio/mpeg";
        case ".mp3":
          return "audio/mpeg";
        case ".mpa":
          return "audio/mpeg";
        case ".mpc":
          return "application/x-project";
        case ".mpe":
          return "video/mpeg";
        case ".mpeg":
          return "video/mpeg";
        case ".mpg":
          return "video/mpeg";
        case ".mpga":
          return "audio/mpeg";
        case ".mpp":
          return "application/vnd.ms-project";
        case ".mpt":
          return "application/vnd.ms-project";
        case ".mpv":
          return "application/vnd.ms-project";
        case ".mpx":
          return "application/vnd.ms-project";
        case ".mrc":
          return "application/marc";
        case ".ms":
          return "application/x-troff-ms";
        case ".mv":
          return "video/x-sgi-movie";
        case ".my":
          return "audio/make";
        case ".mzz":
          return "application/x-vnd.audioexplosion.mzz";
        case ".nap":
          return "image/naplps";
        case ".naplps":
          return "image/naplps";
        case ".nc":
          return "application/x-netcdf";
        case ".ncm":
          return "application/vnd.nokia.configuration-message";
        case ".nif":
          return "image/x-niff";
        case ".niff":
          return "image/x-niff";
        case ".nix":
          return "application/x-mix-transfer";
        case ".nsc":
          return "application/x-conference";
        case ".nvd":
          return "application/x-navidoc";
        case ".o":
          return "application/octet-stream";
        case ".oda":
          return "application/oda";
        case ".omc":
          return "application/x-omc";
        case ".omcd":
          return "application/x-omcdatamaker";
        case ".omcr":
          return "application/x-omcregerator";
        case ".p":
          return "text/x-pascal";
        case ".p10":
          return "application/pkcs10";
        case ".p12":
          return "application/pkcs-12";
        case ".p7a":
          return "application/x-pkcs7-signature";
        case ".p7c":
          return "application/pkcs7-mime";
        case ".p7m":
          return "application/pkcs7-mime";
        case ".p7r":
          return "application/x-pkcs7-certreqresp";
        case ".p7s":
          return "application/pkcs7-signature";
        case ".part":
          return "application/pro_eng";
        case ".pas":
          return "text/pascal";
        case ".pbm":
          return "image/x-portable-bitmap";
        case ".pcl":
          return "application/vnd.hp-pcl";
        case ".pct":
          return "image/x-pict";
        case ".pcx":
          return "image/x-pcx";
        case ".pdb":
          return "chemical/x-pdb";
        case ".pdf":
          return "application/pdf";
        case ".pfunk":
          return "audio/make";
        case ".pgm":
          return "image/x-portable-greymap";
        case ".pic":
          return "image/pict";
        case ".pict":
          return "image/pict";
        case ".pkg":
          return "application/x-newton-compatible-pkg";
        case ".pko":
          return "application/vnd.ms-pki.pko";
        case ".pl":
          return "text/plain";
        case ".plx":
          return "application/x-pixclscript";
        case ".pm":
          return "image/x-xpixmap";
        case ".pm4":
          return "application/x-pagemaker";
        case ".pm5":
          return "application/x-pagemaker";
        case ".png":
          return "image/png";
        case ".pnm":
          return "application/x-portable-anymap";
        case ".pot":
          return "application/vnd.ms-powerpoint";
        case ".pov":
          return "model/x-pov";
        case ".ppa":
          return "application/vnd.ms-powerpoint";
        case ".ppm":
          return "image/x-portable-pixmap";
        case ".pps":
          return "application/vnd.ms-powerpoint";
        case ".ppt":
          return "application/vnd.ms-powerpoint";
        case ".ppz":
          return "application/vnd.ms-powerpoint";
        case ".pre":
          return "application/x-freelance";
        case ".prt":
          return "application/pro_eng";
        case ".ps":
          return "application/postscript";
        case ".psd":
          return "application/octet-stream";
        case ".pvu":
          return "paleovu/x-pv";
        case ".pwz":
          return "application/vnd.ms-powerpoint";
        case ".py":
          return "text/x-script.phyton";
        case ".pyc":
          return "applicaiton/x-bytecode.python";
        case ".qcp":
          return "audio/vnd.qcelp";
        case ".qd3":
          return "x-world/x-3dmf";
        case ".qd3d":
          return "x-world/x-3dmf";
        case ".qif":
          return "image/x-quicktime";
        case ".qt":
          return "video/quicktime";
        case ".qtc":
          return "video/x-qtc";
        case ".qti":
          return "image/x-quicktime";
        case ".qtif":
          return "image/x-quicktime";
        case ".ra":
          return "audio/x-pn-realaudio";
        case ".ram":
          return "audio/x-pn-realaudio";
        case ".ras":
          return "application/x-cmu-raster";
        case ".rast":
          return "image/cmu-raster";
        case ".rexx":
          return "text/x-script.rexx";
        case ".rf":
          return "image/vnd.rn-realflash";
        case ".rgb":
          return "image/x-rgb";
        case ".rm":
          return "application/vnd.rn-realmedia";
        case ".rmi":
          return "audio/mid";
        case ".rmm":
          return "audio/x-pn-realaudio";
        case ".rmp":
          return "audio/x-pn-realaudio";
        case ".rng":
          return "application/ringing-tones";
        case ".rnx":
          return "application/vnd.rn-realplayer";
        case ".roff":
          return "application/x-troff";
        case ".rp":
          return "image/vnd.rn-realpix";
        case ".rpm":
          return "audio/x-pn-realaudio-plugin";
        case ".rt":
          return "text/richtext";
        case ".rtf":
          return "text/richtext";
        case ".rtx":
          return "text/richtext";
        case ".rv":
          return "video/vnd.rn-realvideo";
        case ".s":
          return "text/x-asm";
        case ".s3m":
          return "audio/s3m";
        case ".saveme":
          return "application/octet-stream";
        case ".sbk":
          return "application/x-tbook";
        case ".scm":
          return "application/x-lotusscreencam";
        case ".sdml":
          return "text/plain";
        case ".sdp":
          return "application/sdp";
        case ".sdr":
          return "application/sounder";
        case ".sea":
          return "application/sea";
        case ".set":
          return "application/set";
        case ".sgm":
          return "text/sgml";
        case ".sgml":
          return "text/sgml";
        case ".sh":
          return "application/x-sh";
        case ".shar":
          return "application/x-shar";
        case ".shtml":
          return "text/html";
        case ".sid":
          return "audio/x-psid";
        case ".sit":
          return "application/x-sit";
        case ".skd":
          return "application/x-koan";
        case ".skm":
          return "application/x-koan";
        case ".skp":
          return "application/x-koan";
        case ".skt":
          return "application/x-koan";
        case ".sl":
          return "application/x-seelogo";
        case ".smi":
          return "application/smil";
        case ".smil":
          return "application/smil";
        case ".snd":
          return "audio/basic";
        case ".sol":
          return "application/solids";
        case ".spc":
          return "text/x-speech";
        case ".spl":
          return "application/futuresplash";
        case ".spr":
          return "application/x-sprite";
        case ".sprite":
          return "application/x-sprite";
        case ".src":
          return "application/x-wais-source";
        case ".ssi":
          return "text/x-server-parsed-html";
        case ".ssm":
          return "application/streamingmedia";
        case ".sst":
          return "application/vnd.ms-pki.certstore";
        case ".step":
          return "application/step";
        case ".stl":
          return "application/sla";
        case ".stp":
          return "application/step";
        case ".sv4cpio":
          return "application/x-sv4cpio";
        case ".sv4crc":
          return "application/x-sv4crc";
        case ".svf":
          return "image/vnd.dwg";
        case ".svr":
          return "application/x-world";
        case ".swf":
          return "application/x-shockwave-flash";
        case ".t":
          return "application/x-troff";
        case ".talk":
          return "text/x-speech";
        case ".tar":
          return "application/x-tar";
        case ".tbk":
          return "application/toolbook";
        case ".tcl":
          return "application/x-tcl";
        case ".tcsh":
          return "text/x-script.tcsh";
        case ".tex":
          return "application/x-tex";
        case ".texi":
          return "application/x-texinfo";
        case ".texinfo":
          return "application/x-texinfo";
        case ".text":
          return "text/plain";
        case ".tgz":
          return "application/x-compressed";
        case ".tif":
          return "image/tiff";
        case ".tiff":
          return "image/tiff";
        case ".tr":
          return "application/x-troff";
        case ".tsi":
          return "audio/tsp-audio";
        case ".tsp":
          return "application/dsptype";
        case ".tsv":
          return "text/tab-separated-values";
        case ".turbot":
          return "image/florian";
        case ".txt":
          return "text/plain";
        case ".uil":
          return "text/x-uil";
        case ".uni":
          return "text/uri-list";
        case ".unis":
          return "text/uri-list";
        case ".unv":
          return "application/i-deas";
        case ".uri":
          return "text/uri-list";
        case ".uris":
          return "text/uri-list";
        case ".ustar":
          return "application/x-ustar";
        case ".uu":
          return "application/octet-stream";
        case ".uue":
          return "text/x-uuencode";
        case ".vcd":
          return "application/x-cdlink";
        case ".vcs":
          return "text/x-vcalendar";
        case ".vda":
          return "application/vda";
        case ".vdo":
          return "video/vdo";
        case ".vew":
          return "application/groupwise";
        case ".viv":
          return "video/vivo";
        case ".vivo":
          return "video/vivo";
        case ".vmd":
          return "application/vocaltec-media-desc";
        case ".vmf":
          return "application/vocaltec-media-file";
        case ".voc":
          return "audio/voc";
        case ".vos":
          return "video/vosaic";
        case ".vox":
          return "audio/voxware";
        case ".vqe":
          return "audio/x-twinvq-plugin";
        case ".vqf":
          return "audio/x-twinvq";
        case ".vql":
          return "audio/x-twinvq-plugin";
        case ".vrml":
          return "application/x-vrml";
        case ".vrt":
          return "x-world/x-vrt";
        case ".vsd":
          return "application/x-visio";
        case ".vst":
          return "application/x-visio";
        case ".vsw":
          return "application/x-visio";
        case ".w60":
          return "application/wordperfect6.0";
        case ".w61":
          return "application/wordperfect6.1";
        case ".w6w":
          return "application/msword";
        case ".wav":
          return "audio/wav";
        case ".wb1":
          return "application/x-qpro";
        case ".wbmp":
          return "image/vnd.wap.wbmp";
        case ".web":
          return "application/vnd.xara";
        case ".wiz":
          return "application/msword";
        case ".wk1":
          return "application/x-123";
        case ".wmf":
          return "windows/metafile";
        case ".wml":
          return "text/vnd.wap.wml";
        case ".wmlc":
          return "application/vnd.wap.wmlc";
        case ".wmls":
          return "text/vnd.wap.wmlscript";
        case ".wmlsc":
          return "application/vnd.wap.wmlscriptc";
        case ".word":
          return "application/msword";
        case ".wp":
          return "application/wordperfect";
        case ".wp5":
          return "application/wordperfect";
        case ".wp6":
          return "application/wordperfect";
        case ".wpd":
          return "application/wordperfect";
        case ".wq1":
          return "application/x-lotus";
        case ".wri":
          return "application/mswrite";
        case ".wrl":
          return "application/x-world";
        case ".wrz":
          return "x-world/x-vrml";
        case ".wsc":
          return "text/scriplet";
        case ".wsrc":
          return "application/x-wais-source";
        case ".wtk":
          return "application/x-wintalk";
        case ".xbm":
          return "image/x-xbitmap";
        case ".xdr":
          return "video/x-amt-demorun";
        case ".xgz":
          return "xgl/drawing";
        case ".xif":
          return "image/vnd.xiff";
        case ".xl":
          return "application/excel";
        case ".xla":
          return "application/vnd.ms-excel";
        case ".xlb":
          return "application/vnd.ms-excel";
        case ".xlc":
          return "application/vnd.ms-excel";
        case ".xld":
          return "application/vnd.ms-excel";
        case ".xlk":
          return "application/vnd.ms-excel";
        case ".xll":
          return "application/vnd.ms-excel";
        case ".xlm":
          return "application/vnd.ms-excel";
        case ".xls":
          return "application/vnd.ms-excel";
        case ".xlt":
          return "application/vnd.ms-excel";
        case ".xlv":
          return "application/vnd.ms-excel";
        case ".xlw":
          return "application/vnd.ms-excel";
        case ".xm":
          return "audio/xm";
        case ".xml":
          return "application/xml";
        case ".xmz":
          return "xgl/movie";
        case ".xpix":
          return "application/x-vnd.ls-xpix";
        case ".xpm":
          return "image/xpm";
        case ".x-png":
          return "image/png";
        case ".xsr":
          return "video/x-amt-showrun";
        case ".xwd":
          return "image/x-xwd";
        case ".xyz":
          return "chemical/x-pdb";
        case ".z":
          return "application/x-compressed";
        case ".zip":
          return "application/zip";
        case ".zoo":
          return "application/octet-stream";
        case ".zsh":
          return "text/x-script.zsh";
        default:
          return "application/octet-stream";
      }
    }

    public static string PreventWidows(this string pText, string[] pTags)
    {
      if (string.IsNullOrEmpty(pText))
        return string.Empty;
      return new Regex("<(" + string.Join("|", pTags) + ")>(.+)</\\1>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant).Replace(pText, new MatchEvaluator(WebUtilities.WiDont));
    }

    public static string PreventWidows(this string pText)
    {
      return pText.PreventWidows(WebUtilities._blockTags);
    }

    public static string StripHtml(this string pText)
    {
      if (!string.IsNullOrEmpty(pText))
        return HttpUtility.HtmlDecode(Regex.Replace(pText, "<(.|\\n)*?>", string.Empty));
      return string.Empty;
    }

    public static string XssClean(this string pHtml)
    {
      if (string.IsNullOrEmpty(pHtml))
        return pHtml;
      pHtml = WebUtilities.RemoveInvisibleCharacters(pHtml);
      pHtml = WebUtilities._url.Replace(pHtml, WebUtilities._xssHash + "$1=$2");
      pHtml = WebUtilities._semicolon.Replace(pHtml, "$1;$2");
      pHtml = WebUtilities._utf16.Replace(pHtml, "$1$2;");
      pHtml = pHtml.Replace(WebUtilities._xssHash, "&");
      pHtml = HttpUtility.UrlDecode(pHtml.Replace("+", WebUtilities._xssHash)).Replace(WebUtilities._xssHash, "+");
      pHtml = WebUtilities._entities1.Replace(pHtml, new MatchEvaluator(WebUtilities.ConvertAttribute));
      pHtml = WebUtilities._entities2.Replace(pHtml, (MatchEvaluator) (pX => HttpUtility.HtmlDecode(pX.Value)));
      pHtml = WebUtilities.RemoveInvisibleCharacters(pHtml);
      if (pHtml.IndexOf('\t') != -1)
        pHtml = pHtml.Replace('\t', ' ');
      pHtml = WebUtilities._neverAllowedStr.Aggregate<KeyValuePair<string, string>, string>(pHtml, (Func<string, KeyValuePair<string, string>, string>) ((pCurrent, pNeverAllowedStr) => pCurrent.Replace(pNeverAllowedStr.Key, pNeverAllowedStr.Value)));
      pHtml = WebUtilities._neverAllowedRegex.Aggregate<Regex, string>(pHtml, (Func<string, Regex, string>) ((pCurrent, pNeverAllowedRegex) => pNeverAllowedRegex.Replace(pCurrent, "[removed]")));
      foreach (string str in WebUtilities._expanded)
      {
        StringBuilder stringBuilder = new StringBuilder("#(");
        foreach (char ch in str)
        {
          stringBuilder.Append(ch);
          stringBuilder.Append("\\s*");
        }
        stringBuilder.Remove(stringBuilder.Length - 3, 3);
        stringBuilder.Append(")(\\W)");
        pHtml = new Regex(stringBuilder.ToString(), RegexOptions.IgnoreCase | RegexOptions.Singleline).Replace(pHtml, new MatchEvaluator(WebUtilities.CompactExplodedWords));
      }
      string str1;
      do
      {
        str1 = pHtml;
        if (pHtml.IndexOf("<a", StringComparison.OrdinalIgnoreCase) != -1)
          goto label_15;
label_10:
        if (pHtml.IndexOf("<img", StringComparison.OrdinalIgnoreCase) != -1)
          pHtml = WebUtilities._imgReplaceTag.Replace(pHtml, new MatchEvaluator(WebUtilities.JsImgRemoval));
        if (pHtml.IndexOf("script", StringComparison.OrdinalIgnoreCase) != -1 || pHtml.IndexOf("xss", StringComparison.OrdinalIgnoreCase) != -1)
          pHtml = WebUtilities._scriptReplaceTag.Replace(pHtml, "[removed]");
        continue;
label_15:
        pHtml = WebUtilities._aReplaceTag.Replace(pHtml, new MatchEvaluator(WebUtilities.JsLinkRemoval));
        goto label_10;
      }
      while (!str1.Equals(pHtml, StringComparison.Ordinal));
      pHtml = WebUtilities._removeHandlers.Replace(pHtml, "<$1$4");
      pHtml = WebUtilities._removeNaughtyHtml.Replace(pHtml, new MatchEvaluator(WebUtilities.SanitizeNaughtyHtml));
      pHtml = WebUtilities._removeNaughtyScript.Replace(pHtml, "$1$2&#40;$3&#41;");
      pHtml = WebUtilities._neverAllowedStr.Aggregate<KeyValuePair<string, string>, string>(pHtml, (Func<string, KeyValuePair<string, string>, string>) ((pCurrent, pNeverAllowedStr) => pCurrent.Replace(pNeverAllowedStr.Key, pNeverAllowedStr.Value)));
      return WebUtilities._neverAllowedRegex.Aggregate<Regex, string>(pHtml, (Func<string, Regex, string>) ((pCurrent, pNeverAllowedRegex) => pNeverAllowedRegex.Replace(pCurrent, "[removed]")));
    }

    private static string CompactExplodedWords(Match pMatch)
    {
      return WebUtilities._compact.Replace(pMatch.Groups[0].Value, "") + pMatch.Groups[1].Value;
    }

    private static string ConvertAttribute(Match pMatch)
    {
      return pMatch.Value.Replace(">", "&gt;").Replace("<", "&lt;").Replace("\\", "\\\\");
    }

    private static string FilterAttributes(string pAttributes)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Match match in WebUtilities._attrFilter.Matches(pAttributes))
        stringBuilder.Append(WebUtilities._attrFilterReplace.Replace(match.Value, ""));
      return stringBuilder.ToString();
    }

    private static CompressionMethod GetFilterForScheme(
      IEnumerable<string> pSchemes)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      foreach (string pAcceptEncodingValue in pSchemes.Where<string>((Func<string, bool>) (acceptEncoding => !string.IsNullOrEmpty(acceptEncoding))).Select<string, string>((Func<string, string>) (acceptEncoding => acceptEncoding.Trim().ToLowerInvariant())))
      {
        if (pAcceptEncodingValue.StartsWith("deflate", StringComparison.OrdinalIgnoreCase))
        {
          flag1 = true;
          float quality = WebUtilities.GetQuality(pAcceptEncodingValue);
          if ((double) num1 < (double) quality)
            num1 = quality;
        }
        else if (!pAcceptEncodingValue.StartsWith("gzip", StringComparison.OrdinalIgnoreCase) && !pAcceptEncodingValue.StartsWith("x-gzip", StringComparison.OrdinalIgnoreCase))
        {
          if (pAcceptEncodingValue.StartsWith("*", StringComparison.Ordinal))
          {
            flag3 = true;
            float quality = WebUtilities.GetQuality(pAcceptEncodingValue);
            if ((double) num3 < (double) quality)
              num3 = quality;
          }
        }
        else
        {
          flag2 = true;
          float quality = WebUtilities.GetQuality(pAcceptEncodingValue);
          if ((double) num2 < (double) quality)
            num2 = quality;
        }
      }
      bool flag4 = flag3 && (double) num3 > 0.0;
      bool flag5 = flag1 && (double) num1 > 0.0 || !flag1 && flag4;
      bool flag6 = flag2 && (double) num2 > 0.0 || !flag2 && flag4;
      if (flag5 && !flag1)
        num1 = num3;
      if (flag6 && !flag2)
        num2 = num3;
      if (!flag5 && !flag6 && !flag4)
        return CompressionMethod.None;
      if (flag5 && (!flag6 || (double) num1 >= (double) num2))
        return CompressionMethod.Deflate;
      return flag6 && (!flag5 || (double) num1 <= (double) num2) || flag6 ? CompressionMethod.GZip : CompressionMethod.None;
    }

    private static float GetQuality(string pAcceptEncodingValue)
    {
      int num1 = pAcceptEncodingValue.IndexOf("q=", StringComparison.OrdinalIgnoreCase);
      if (num1 < 0)
        return 1f;
      float num2 = 0.0f;
      try
      {
        num2 = float.Parse(pAcceptEncodingValue.Substring(num1 + 2, pAcceptEncodingValue.Length - (num1 + 2)));
      }
      catch (FormatException ex)
      {
      }
      return num2;
    }

    private static string JsImgRemoval(Match pMatch)
    {
      string input = WebUtilities.FilterAttributes(pMatch.Groups[1].Value.Replace("<", "").Replace(">", ""));
      string newValue = WebUtilities._attrFilterImg.Replace(input, "");
      return pMatch.Groups[0].Value.Replace(pMatch.Groups[1].Value, newValue);
    }

    private static string JsLinkRemoval(Match pMatch)
    {
      string input = WebUtilities.FilterAttributes(pMatch.Groups[1].Value.Replace("<", "").Replace(">", ""));
      string newValue = WebUtilities._attrFilterLink.Replace(input, "");
      return pMatch.Groups[0].Value.Replace(pMatch.Groups[1].Value, newValue);
    }

    private static string RemoveInvisibleCharacters(string pIn)
    {
      string str;
      do
      {
        str = pIn;
        pIn = WebUtilities._nonDisplayables.Aggregate<Regex, string>(pIn, (Func<string, Regex, string>) ((pCurrent, pNonDisplayable) => pNonDisplayable.Replace(pCurrent, "")));
      }
      while (!str.Equals(pIn, StringComparison.Ordinal));
      return pIn;
    }

    private static string SanitizeNaughtyHtml(Match pMatch)
    {
      return "&lt;" + pMatch.Groups[1].Value + pMatch.Groups[2].Value + pMatch.Groups[3].Value + pMatch.Groups[4].Value.Replace("<", "&lt;").Replace(">", "&gt;");
    }

    private static string WiDont(Match pM)
    {
      string str = pM.Value.TrimEnd();
      int length = str.LastIndexOf(' ');
      if (length != -1)
        str = str.Substring(0, length) + "&nbsp;" + str.Substring(length + 1);
      return str;
    }
  }
}
