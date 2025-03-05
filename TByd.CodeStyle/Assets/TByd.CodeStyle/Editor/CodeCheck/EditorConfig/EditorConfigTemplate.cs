using System.Collections.Generic;

namespace TByd.CodeStyle.Editor.CodeCheck.EditorConfig
{
    /// <summary>
    /// EditorConfig模板
    /// </summary>
    public static class EditorConfigTemplate
    {
        /// <summary>
        /// 获取默认的EditorConfig规则
        /// </summary>
        /// <returns>默认的EditorConfig规则列表</returns>
        public static List<EditorConfigRule> GetDefaultRules()
        {
            var rules = new List<EditorConfigRule>();

            // 所有文件的通用规则
            var allFilesRule = new EditorConfigRule("*");
            allFilesRule.SetProperty("charset", "utf-8");
            allFilesRule.SetProperty("end_of_line", "lf");
            allFilesRule.SetProperty("insert_final_newline", "true");
            allFilesRule.SetProperty("trim_trailing_whitespace", "true");
            rules.Add(allFilesRule);

            // C#文件规则
            var csharpRule = new EditorConfigRule("*.cs");
            csharpRule.SetProperty("indent_style", "space");
            csharpRule.SetProperty("indent_size", "4");
            csharpRule.SetProperty("tab_width", "4");
            rules.Add(csharpRule);

            // JSON文件规则
            var jsonRule = new EditorConfigRule("*.json");
            jsonRule.SetProperty("indent_style", "space");
            jsonRule.SetProperty("indent_size", "2");
            rules.Add(jsonRule);

            // YAML文件规则
            var yamlRule = new EditorConfigRule("*.{yml,yaml}");
            yamlRule.SetProperty("indent_style", "space");
            yamlRule.SetProperty("indent_size", "2");
            rules.Add(yamlRule);

            // Markdown文件规则
            var markdownRule = new EditorConfigRule("*.md");
            markdownRule.SetProperty("trim_trailing_whitespace", "false");
            rules.Add(markdownRule);

            // Unity特定文件规则
            var unityRule = new EditorConfigRule("*.{asmdef,asmref}");
            unityRule.SetProperty("indent_style", "space");
            unityRule.SetProperty("indent_size", "4");
            rules.Add(unityRule);

            return rules;
        }

        /// <summary>
        /// 获取Unity项目推荐的EditorConfig规则
        /// </summary>
        /// <returns>Unity项目推荐的EditorConfig规则列表</returns>
        public static List<EditorConfigRule> GetUnityProjectRules()
        {
            var rules = new List<EditorConfigRule>();

            // 所有文件的通用规则
            var allFilesRule = new EditorConfigRule("*");
            allFilesRule.SetProperty("charset", "utf-8");
            allFilesRule.SetProperty("end_of_line", "lf");
            allFilesRule.SetProperty("insert_final_newline", "true");
            allFilesRule.SetProperty("trim_trailing_whitespace", "true");
            rules.Add(allFilesRule);

            // C#文件规则
            var csharpRule = new EditorConfigRule("*.cs");
            csharpRule.SetProperty("indent_style", "space");
            csharpRule.SetProperty("indent_size", "4");
            csharpRule.SetProperty("tab_width", "4");

            // C#编码常规约定
            csharpRule.SetProperty("dotnet_sort_system_directives_first", "true");
            csharpRule.SetProperty("dotnet_style_qualification_for_field", "false:silent");
            csharpRule.SetProperty("dotnet_style_qualification_for_property", "false:silent");
            csharpRule.SetProperty("dotnet_style_qualification_for_method", "false:silent");
            csharpRule.SetProperty("dotnet_style_qualification_for_event", "false:silent");
            csharpRule.SetProperty("dotnet_style_predefined_type_for_locals_parameters_members", "true:silent");
            csharpRule.SetProperty("dotnet_style_predefined_type_for_member_access", "true:silent");
            csharpRule.SetProperty("dotnet_style_parentheses_in_arithmetic_binary_operators",
                "always_for_clarity:silent");
            csharpRule.SetProperty("dotnet_style_parentheses_in_relational_binary_operators",
                "always_for_clarity:silent");
            csharpRule.SetProperty("dotnet_style_parentheses_in_other_binary_operators", "always_for_clarity:silent");
            csharpRule.SetProperty("dotnet_style_parentheses_in_other_operators", "never_if_unnecessary:silent");
            csharpRule.SetProperty("dotnet_style_require_accessibility_modifiers", "for_non_interface_members:silent");
            csharpRule.SetProperty("dotnet_style_readonly_field", "true:suggestion");
            csharpRule.SetProperty("dotnet_style_object_initializer", "true:suggestion");
            csharpRule.SetProperty("dotnet_style_collection_initializer", "true:suggestion");
            csharpRule.SetProperty("dotnet_style_explicit_tuple_names", "true:suggestion");
            csharpRule.SetProperty("dotnet_style_null_propagation", "true:suggestion");
            csharpRule.SetProperty("dotnet_style_coalesce_expression", "true:suggestion");
            csharpRule.SetProperty("dotnet_style_prefer_is_null_check_over_reference_equality_method", "true:silent");
            csharpRule.SetProperty("dotnet_style_prefer_inferred_tuple_names", "true:suggestion");
            csharpRule.SetProperty("dotnet_style_prefer_inferred_anonymous_type_member_names", "true:suggestion");
            csharpRule.SetProperty("dotnet_style_prefer_auto_properties", "true:silent");
            csharpRule.SetProperty("dotnet_style_prefer_conditional_expression_over_assignment", "true:silent");
            csharpRule.SetProperty("dotnet_style_prefer_conditional_expression_over_return", "true:silent");

            // var使用建议
            csharpRule.SetProperty("csharp_style_var_for_built_in_types", "true:suggestion");
            csharpRule.SetProperty("csharp_style_var_when_type_is_apparent", "true:suggestion");
            csharpRule.SetProperty("csharp_style_var_elsewhere", "true:suggestion");

            // 表达式相关
            csharpRule.SetProperty("csharp_style_expression_bodied_methods", "false:silent");
            csharpRule.SetProperty("csharp_style_expression_bodied_constructors", "false:silent");
            csharpRule.SetProperty("csharp_style_expression_bodied_operators", "false:silent");
            csharpRule.SetProperty("csharp_style_expression_bodied_properties", "true:silent");
            csharpRule.SetProperty("csharp_style_expression_bodied_indexers", "true:silent");
            csharpRule.SetProperty("csharp_style_expression_bodied_accessors", "true:silent");
            csharpRule.SetProperty("csharp_style_pattern_matching_over_is_with_cast_check", "true:suggestion");
            csharpRule.SetProperty("csharp_style_pattern_matching_over_as_with_null_check", "true:suggestion");
            csharpRule.SetProperty("csharp_style_throw_expression", "true:suggestion");
            csharpRule.SetProperty("csharp_style_conditional_delegate_call", "true:suggestion");
            csharpRule.SetProperty("csharp_preferred_modifier_order",
                "public,private,protected,internal,static,extern,new,virtual,abstract,sealed,override,readonly,unsafe,volatile,async:suggestion");
            csharpRule.SetProperty("csharp_prefer_braces", "true:suggestion");
            csharpRule.SetProperty("csharp_prefer_simple_default_expression", "true:suggestion");
            csharpRule.SetProperty("csharp_style_prefer_local_over_anonymous_function", "true:suggestion");
            csharpRule.SetProperty("csharp_style_inlined_variable_declaration", "true:suggestion");

            // C#代码风格规则
            csharpRule.SetProperty("csharp_new_line_before_open_brace", "all");
            csharpRule.SetProperty("csharp_new_line_before_else", "true");
            csharpRule.SetProperty("csharp_new_line_before_catch", "true");
            csharpRule.SetProperty("csharp_new_line_before_finally", "true");
            csharpRule.SetProperty("csharp_new_line_before_members_in_object_initializers", "true");
            csharpRule.SetProperty("csharp_new_line_before_members_in_anonymous_types", "true");
            csharpRule.SetProperty("csharp_new_line_between_query_expression_clauses", "true");
            csharpRule.SetProperty("csharp_preserve_single_line_statements", "false");
            csharpRule.SetProperty("csharp_preserve_single_line_blocks", "false");

            // 缩进规则
            csharpRule.SetProperty("csharp_indent_case_contents", "true");
            csharpRule.SetProperty("csharp_indent_case_contents_when_block", "false");
            csharpRule.SetProperty("csharp_indent_switch_labels", "true");
            csharpRule.SetProperty("csharp_indent_labels", "flush_left");
            csharpRule.SetProperty("csharp_indent_block_contents", "true");
            csharpRule.SetProperty("csharp_indent_braces", "false");

            // 空格规则
            csharpRule.SetProperty("csharp_space_after_cast", "false");
            csharpRule.SetProperty("csharp_space_after_keywords_in_control_flow_statements", "true");
            csharpRule.SetProperty("csharp_space_between_method_declaration_parameter_list_parentheses", "false");
            csharpRule.SetProperty("csharp_space_between_method_call_parameter_list_parentheses", "false");
            csharpRule.SetProperty("csharp_space_between_parentheses", "false");
            csharpRule.SetProperty("csharp_space_before_colon_in_inheritance_clause", "true");
            csharpRule.SetProperty("csharp_space_after_colon_in_inheritance_clause", "true");
            csharpRule.SetProperty("csharp_space_around_binary_operators", "before_and_after");
            csharpRule.SetProperty("csharp_space_between_method_declaration_empty_parameter_list_parentheses", "false");
            csharpRule.SetProperty("csharp_space_between_method_call_name_and_opening_parenthesis", "false");
            csharpRule.SetProperty("csharp_space_between_method_call_empty_parameter_list_parentheses", "false");
            csharpRule.SetProperty("csharp_space_after_comma", "true");
            csharpRule.SetProperty("csharp_space_before_comma", "false");
            csharpRule.SetProperty("csharp_space_after_dot", "false");
            csharpRule.SetProperty("csharp_space_before_dot", "false");
            csharpRule.SetProperty("csharp_space_before_semicolon_in_for_statement", "false");
            csharpRule.SetProperty("csharp_space_after_semicolon_in_for_statement", "true");
            csharpRule.SetProperty("csharp_space_around_declaration_statements", "false");
            csharpRule.SetProperty("csharp_space_before_open_square_brackets", "false");
            csharpRule.SetProperty("csharp_space_between_empty_square_brackets", "false");
            csharpRule.SetProperty("csharp_space_between_square_brackets", "false");

            // 命名规则 - 使用Pascal命名规则的符号
            csharpRule.SetProperty("dotnet_naming_style.pascal_case_style.capitalization", "pascal_case");
            csharpRule.SetProperty("dotnet_naming_symbols.pascal_case_symbols.applicable_kinds",
                "namespace,class,struct,enum,property,method,event,delegate");
            csharpRule.SetProperty("dotnet_naming_symbols.pascal_case_symbols.applicable_accessibilities", "*");
            csharpRule.SetProperty("dotnet_naming_rule.pascal_case_symbols_should_be_pascal_case.severity",
                "suggestion");
            csharpRule.SetProperty("dotnet_naming_rule.pascal_case_symbols_should_be_pascal_case.symbols",
                "pascal_case_symbols");
            csharpRule.SetProperty("dotnet_naming_rule.pascal_case_symbols_should_be_pascal_case.style",
                "pascal_case_style");

            // 命名规则 - 公开成员变量使用驼峰式命名
            csharpRule.SetProperty("dotnet_naming_style.public_class_member_style.capitalization", "camel_case");
            csharpRule.SetProperty("dotnet_naming_symbols.public_class_member.applicable_kinds", "field");
            csharpRule.SetProperty("dotnet_naming_symbols.public_class_member.applicable_accessibilities", "public");
            csharpRule.SetProperty("dotnet_naming_rule.public_class_member_rule.severity", "suggestion");
            csharpRule.SetProperty("dotnet_naming_rule.public_class_member_rule.symbols", "public_class_member");
            csharpRule.SetProperty("dotnet_naming_rule.public_class_member_rule.style", "public_class_member_style");

            // 命名规则 - 静态成员变量使用s_前缀
            csharpRule.SetProperty("dotnet_naming_style.static_member_style.capitalization", "pascal_case");
            csharpRule.SetProperty("dotnet_naming_style.static_member_style.required_prefix", "s_");
            csharpRule.SetProperty("dotnet_naming_symbols.static_member.applicable_kinds", "field");
            csharpRule.SetProperty("dotnet_naming_symbols.static_member.applicable_accessibilities", "*");
            csharpRule.SetProperty("dotnet_naming_symbols.static_member.required_modifiers", "static");
            csharpRule.SetProperty("dotnet_naming_rule.static_member_rule.severity", "suggestion");
            csharpRule.SetProperty("dotnet_naming_rule.static_member_rule.symbols", "static_member");
            csharpRule.SetProperty("dotnet_naming_rule.static_member_rule.style", "static_member_style");

            // 命名规则 - 私有成员变量使用m_前缀
            csharpRule.SetProperty("dotnet_naming_style.private_class_member_style.capitalization", "pascal_case");
            csharpRule.SetProperty("dotnet_naming_style.private_class_member_style.required_prefix", "m_");
            csharpRule.SetProperty("dotnet_naming_symbols.private_class_member.applicable_kinds", "field");
            csharpRule.SetProperty("dotnet_naming_symbols.private_class_member.applicable_accessibilities",
                "internal,friend,private,protected,protected_internal,protected_friend,private_protected");
            csharpRule.SetProperty("dotnet_naming_rule.private_class_member_rule.severity", "suggestion");
            csharpRule.SetProperty("dotnet_naming_rule.private_class_member_rule.symbols", "private_class_member");
            csharpRule.SetProperty("dotnet_naming_rule.private_class_member_rule.style", "private_class_member_style");

            // 命名规则 - 常量使用全大写字母
            csharpRule.SetProperty("dotnet_naming_style.constant_fields_style.capitalization", "all_upper");
            csharpRule.SetProperty("dotnet_naming_style.constant_fields_style.word_separator", "_");
            csharpRule.SetProperty("dotnet_naming_symbols.constant_fields.applicable_kinds", "field");
            csharpRule.SetProperty("dotnet_naming_symbols.constant_fields.applicable_accessibilities", "*");
            csharpRule.SetProperty("dotnet_naming_symbols.constant_fields.required_modifiers", "const");
            csharpRule.SetProperty("dotnet_naming_rule.constant_fields_rule.severity", "suggestion");
            csharpRule.SetProperty("dotnet_naming_rule.constant_fields_rule.symbols", "constant_fields");
            csharpRule.SetProperty("dotnet_naming_rule.constant_fields_rule.style", "constant_fields_style");

            // 命名规则 - 接口使用I前缀
            csharpRule.SetProperty("dotnet_naming_style.interface_style.capitalization", "pascal_case");
            csharpRule.SetProperty("dotnet_naming_style.interface_style.required_prefix", "I");
            csharpRule.SetProperty("dotnet_naming_symbols.interface_symbol.applicable_kinds", "interface");
            csharpRule.SetProperty("dotnet_naming_symbols.interface_symbol.applicable_accessibilities", "*");
            csharpRule.SetProperty("dotnet_naming_rule.interface_should_be_pascal_case.severity", "suggestion");
            csharpRule.SetProperty("dotnet_naming_rule.interface_should_be_pascal_case.symbols", "interface_symbol");
            csharpRule.SetProperty("dotnet_naming_rule.interface_should_be_pascal_case.style", "interface_style");

            rules.Add(csharpRule);

            // JSON文件规则
            var jsonRule = new EditorConfigRule("*.json");
            jsonRule.SetProperty("indent_style", "space");
            jsonRule.SetProperty("indent_size", "2");
            rules.Add(jsonRule);

            // YAML文件规则
            var yamlRule = new EditorConfigRule("*.{yml,yaml}");
            yamlRule.SetProperty("indent_style", "space");
            yamlRule.SetProperty("indent_size", "2");
            rules.Add(yamlRule);

            // Markdown文件规则
            var markdownRule = new EditorConfigRule("*.md");
            markdownRule.SetProperty("trim_trailing_whitespace", "false");
            rules.Add(markdownRule);

            // Unity特定文件规则
            var unityRule = new EditorConfigRule("*.{asmdef,asmref}");
            unityRule.SetProperty("indent_style", "space");
            unityRule.SetProperty("indent_size", "4");
            rules.Add(unityRule);

            // Unity序列化文件规则
            var unityYamlRule = new EditorConfigRule("*.unity");
            unityYamlRule.SetProperty("indent_style", "space");
            unityYamlRule.SetProperty("indent_size", "2");
            rules.Add(unityYamlRule);

            var unityMetaRule = new EditorConfigRule("*.meta");
            unityMetaRule.SetProperty("indent_style", "space");
            unityMetaRule.SetProperty("indent_size", "2");
            rules.Add(unityMetaRule);

            var unityPrefabRule = new EditorConfigRule("*.prefab");
            unityPrefabRule.SetProperty("indent_style", "space");
            unityPrefabRule.SetProperty("indent_size", "2");
            rules.Add(unityPrefabRule);

            // Makefile规则
            var makefileRule = new EditorConfigRule("Makefile");
            makefileRule.SetProperty("indent_style", "tab");
            rules.Add(makefileRule);

            return rules;
        }

        /// <summary>
        /// 获取默认的EditorConfig内容
        /// </summary>
        /// <returns>默认的EditorConfig内容</returns>
        public static string GetDefaultContent()
        {
            return EditorConfigParser.GenerateContent(GetDefaultRules());
        }

        /// <summary>
        /// 获取Unity项目推荐的EditorConfig内容
        /// </summary>
        /// <returns>Unity项目推荐的EditorConfig内容</returns>
        public static string GetUnityProjectContent()
        {
            return EditorConfigParser.GenerateContent(GetUnityProjectRules());
        }
    }
}
