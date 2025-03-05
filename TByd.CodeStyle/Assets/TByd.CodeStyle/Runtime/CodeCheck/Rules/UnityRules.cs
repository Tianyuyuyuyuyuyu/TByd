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
    /// 避免在Update中使用GetComponent规则
    /// </summary>
    public class AvoidGetComponentInUpdateRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "unity-avoid-getcomponent-in-update";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "避免在Update中使用GetComponent规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免在Update方法中使用GetComponent，因为它性能较低";

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
        protected override string Pattern => @"void\s+Update\s*\(\s*\)[^}]*GetComponent\s*<";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免在Update方法中使用GetComponent，这会导致性能问题";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "在Awake或Start方法中缓存组件引用，而不是在Update中重复获取";
    }

    /// <summary>
    /// 避免在Update中使用Find规则
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
        public override string Name => "避免在Update中使用Find规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免在Update方法中使用Find方法，因为它性能较低";

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
        protected override string Pattern => @"void\s+Update\s*\(\s*\)[^}]*\.Find\s*\(";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免在Update方法中使用Find方法，这会导致性能问题";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "在Awake或Start方法中查找并缓存对象引用，而不是在Update中重复查找";
    }

    /// <summary>
    /// 避免使用SendMessage规则
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
        public override string Description => "避免使用SendMessage方法，因为它性能较低和类型不安全";

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
        protected override string Pattern => @"\.SendMessage\s*\(";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用SendMessage方法，它性能较低且类型不安全";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "考虑使用事件、接口或直接方法调用代替SendMessage";
    }

    /// <summary>
    /// 避免使用字符串版本协程规则
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
        public override string Name => "避免使用字符串版本协程规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免使用字符串版本的StartCoroutine方法，因为它性能较低和类型不安全";

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
        protected override string Pattern => @"StartCoroutine\s*\(\s*""[^""]+""";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用字符串版本的StartCoroutine方法，它性能较低且类型不安全";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "使用方法引用版本的StartCoroutine代替字符串版本";
    }

    /// <summary>
    /// 避免硬编码路径规则
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
        public override string Name => "避免硬编码路径规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免在代码中硬编码资源路径，这不利于维护和重构";

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
        protected override string Pattern => @"(Resources\.Load|AssetDatabase\.LoadAssetAtPath|File\.(Open|Read|Write|Exists|ReadAllText|WriteAllText|ReadAllBytes|WriteAllBytes|Copy|Move|Delete)|Directory\.(Create|Delete|Exists|GetFiles|GetDirectories)|Path\.Combine)\s*\(\s*""[^""]*(/|\\)[^""]*""";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免硬编码资源路径，这会导致维护困难和重构风险";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "使用常量或配置文件存储路径，或考虑使用资源引用";
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
        public override string Description => "避免使用OnGUI方法，它在每帧都会创建GC垃圾并且性能较低";

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
        protected override string Pattern => @"void\s+OnGUI\s*\(";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "避免使用OnGUI方法，它会产生大量GC垃圾并影响性能";

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
        public override string Description => "避免频繁使用Camera.main，它在内部使用FindGameObjectWithTag";

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
        protected override string IssueMessageTemplate => "避免频繁使用Camera.main，它在内部使用FindGameObjectWithTag导致性能开销";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "在Awake或Start中缓存对主摄像机的引用，而不是重复访问Camera.main";
    }

    /// <summary>
    /// 避免空MonoBehaviour方法规则
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
        public override string Name => "避免空MonoBehaviour方法规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "避免使用空的MonoBehaviour方法，它们仍会被Unity调用并产生额外开销";

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
        protected override string Pattern => @"void\s+(Awake|Start|Update|FixedUpdate|LateUpdate)\s*\(\s*\)\s*{\s*}";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "空的MonoBehaviour方法仍会被Unity调用，产生额外开销";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "移除空的MonoBehaviour方法以减少开销";
    }
}
