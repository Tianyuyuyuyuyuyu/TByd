using System.Text.RegularExpressions;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Runtime.CodeCheck.Rules
{
    /// <summary>
    /// 避免使用GameObject.Find规则
    /// </summary>
    public class AvoidGameObjectFindRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-gameobject-find";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免使用GameObject.Find规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免使用GameObject.Find方法，因为它性能较低";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Unity;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"GameObject\.Find\s*\(";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用GameObject.Find方法，因为它性能较低";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "考虑使用引用、缓存或其他方式获取GameObject";
    }

    /// <summary>
    /// 避免在Update中使用查找方法规则
    /// </summary>
    public class AvoidFindInUpdateRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-find-in-update";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免在Update中使用查找方法规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免在Update、FixedUpdate或LateUpdate方法中使用查找方法";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Unity;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Error;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"(void\s+(Update|FixedUpdate|LateUpdate)\s*\(\s*\)\s*\{[^}]*)(Find|GetComponent)";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免在{1}方法中使用{2}方法，这会导致性能问题";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "将{2}方法移到Awake或Start方法中，并缓存结果";
    }

    /// <summary>
    /// 避免使用字符串比较的SendMessage规则
    /// </summary>
    public class AvoidSendMessageRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-sendmessage";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免使用SendMessage规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免使用SendMessage方法，因为它使用字符串比较且性能较低";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Unity;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"\.(SendMessage|BroadcastMessage|SendMessageUpwards)\s*\(";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用{0}方法，因为它使用字符串比较且性能较低";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "考虑使用直接方法调用、事件或接口代替{0}";
    }

    /// <summary>
    /// 避免使用协程字符串规则
    /// </summary>
    public class AvoidCoroutineStringRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-coroutine-string";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免使用协程字符串规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免使用字符串名称启动协程，应使用方法引用";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Unity;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"StartCoroutine\s*\(\s*""";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用字符串名称启动协程，这不是类型安全的";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "使用方法引用代替字符串，例如：StartCoroutine(MethodName())";
    }

    /// <summary>
    /// 避免使用硬编码路径规则
    /// </summary>
    public class AvoidHardcodedPathRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-hardcoded-path";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免使用硬编码路径规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免在代码中使用硬编码的资源路径";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Unity;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"(Resources\.Load|AssetDatabase\.LoadAssetAtPath)\s*\(\s*""[^""]+""";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用硬编码的资源路径，这会导致维护问题";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "使用常量或配置文件存储路径，或考虑使用Addressables系统";
    }

    /// <summary>
    /// 避免使用OnGUI规则
    /// </summary>
    public class AvoidOnGUIRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-ongui";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免使用OnGUI规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免使用OnGUI方法，因为它性能较低";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Unity;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"void\s+OnGUI\s*\(\s*\)";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用OnGUI方法，因为它性能较低";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "考虑使用Unity UI系统(uGUI)或UI Toolkit代替IMGUI";
    }

    /// <summary>
    /// 避免使用Camera.main规则
    /// </summary>
    public class AvoidCameraMainRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-camera-main";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免使用Camera.main规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免频繁使用Camera.main，因为它在内部使用FindGameObjectWithTag";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Unity;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"Camera\.main";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免频繁使用Camera.main，因为它在内部使用FindGameObjectWithTag";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "在Awake或Start中缓存主摄像机引用";
    }

    /// <summary>
    /// 避免使用空MonoBehaviour方法规则
    /// </summary>
    public class AvoidEmptyMonoBehaviourMethodsRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-empty-monobehaviour-methods";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免使用空MonoBehaviour方法规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免使用空的MonoBehaviour方法，如空的Update或Start";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Unity;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Info;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"void\s+(Start|Update|FixedUpdate|LateUpdate|Awake)\s*\(\s*\)\s*\{\s*\}";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用空的{1}方法，这会导致不必要的性能开销";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "移除空的{1}方法";
    }
}
