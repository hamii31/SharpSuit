using System.Collections.Immutable;

namespace SharpSuit.Apps.SharpXSS.AppConstants
{
    public class PathConstants
    {
        public class FirefoxConstants
        {
            public const string firefoxDriverPath = @"C:\Users\Hami\Downloads\geckodriver-v0.35.0-win32\";
            public const string firefoxBinaryLocationPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
        }

        public class OutputConstants
        {
            public const string outputXSSPath = @"C:../../../Payloads/output_xss";
        }
        public class PayloadConstants
        {
            public const string audioXSSPath = @"C:../../../Payloads/audio_xss.txt";
            public const string basicXSSPath = @"C:../../../Payloads/basic_xss.txt";
            public const string alertXSSPath = @"C:../../../Payloads/alert_xss.txt";
            public const string bodyXSSPath = @"C:../../../Payloads/body_xss.txt";
            public const string cloudflareXSSPath = @"C:../../../Payloads/cloudflare_xss.txt";
            public const string divXSSPath = @"C:../../../Payloads/div_xss.txt";
            public const string imgXSSPath = @"C:../../../Payloads/img_xss.txt";
            public const string polyglotXSSPath = @"C:../../../Payloads/polyglot_xss.txt";
            public const string svgXSSPath = @"C:../../../Payloads/svg_xss.txt";
            public const string wafXSSPath = @"C:../../../Payloads/waf_bypass_xss.txt";
            public const string akamaiXSSPath = @"C:../../../Payloads/akamai_bypass_xss.txt";
            public const string cloudfrontXSSPath = @"C:../../../Payloads/cloudfront_xss.txt";
            public const string impervaXSSPath = @"C:../../../Payloads/imperva_xss.txt";
            public const string incapsulaXSSPath = @"C:../../../Payloads/incapsula_xss.txt";
            public const string wordfenceXSSPath = @"C:../../../Payloads/wordfence_xss.txt";
        }
    }
    public class PayloadOptions
    {
        public static ImmutableArray<string> Payloads = ImmutableArray.Create(
                "basic xss", "alert xss", "body tag xss", "div tag xss", "img tag xss", "svg tag xss", "polyglot xss",
                "basic waf bypass xss", "cloudflare bypass xss", "cloudfront bypass xss", "akamai bypass xss", "imperva bypass xss",
                "incapsula bypass xss", "wordfence bypass xss"
            );
    }
}
