﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace Microsoft.AspNetCore.Blazor.Razor
{
    internal static class BlazorDiagnosticFactory
    {
        public static readonly RazorDiagnosticDescriptor InvalidComponentAttributeSyntax = new RazorDiagnosticDescriptor(
            "BL9980",
            () => "Wrong syntax for '{0}' on '{1}': As a temporary " +
                $"limitation, component attributes must be expressed with C# syntax. For example, " +
                $"SomeParam=@(\"Some value\") is allowed, but SomeParam=\"Some value\" is not.",
            RazorDiagnosticSeverity.Error);

        public static RazorDiagnostic Create_InvalidComponentAttributeSynx(TextPosition position, SourceSpan? span, string attributeName, string componentName)
        {
            span = CalculateSourcePosition(span, position);
            return RazorDiagnostic.Create(InvalidComponentAttributeSyntax, span ?? SourceSpan.Undefined, attributeName, componentName);
        }

        public static readonly RazorDiagnosticDescriptor UnexpectedClosingTag = new RazorDiagnosticDescriptor(
            "BL9981",
            () => "Unexpected closing tag '{0}' with no matching start tag.",
            RazorDiagnosticSeverity.Error);

        public static RazorDiagnostic Create_UnexpectedClosingTag(SourceSpan? span, string tagName)
        {
            return RazorDiagnostic.Create(UnexpectedClosingTag, span ?? SourceSpan.Undefined, tagName);
        }

        public static readonly RazorDiagnosticDescriptor MismatchedClosingTag = new RazorDiagnosticDescriptor(
            "BL9982",
            () => "Mismatching closing tag. Found '{0}' but expected '{1}'.",
            RazorDiagnosticSeverity.Error);

        public static RazorDiagnostic Create_MismatchedClosingTag(SourceSpan? span, string expectedTagName, string tagName)
        {
            return RazorDiagnostic.Create(MismatchedClosingTag, span ?? SourceSpan.Undefined, expectedTagName, tagName);
        }

        public static readonly RazorDiagnosticDescriptor MismatchedClosingTagKind = new RazorDiagnosticDescriptor(
            "BL9984",
            () => "Mismatching closing tag. Found '{0}' of type '{1}' but expected type '{2}'.",
            RazorDiagnosticSeverity.Error);

        public static RazorDiagnostic Create_MismatchedClosingTagKind(SourceSpan? span, string tagName, string kind, string expectedKind)
        {
            return RazorDiagnostic.Create(MismatchedClosingTagKind, span ?? SourceSpan.Undefined, tagName, kind, expectedKind);
        }

        public static readonly RazorDiagnosticDescriptor MultipleComponents = new RazorDiagnosticDescriptor(
            "BL9985",
            () => "Multiple components use the tag '{0}'. Components: {1}",
            RazorDiagnosticSeverity.Error);

        public static RazorDiagnostic Create_MultipleComponents(SourceSpan? span, string tagName, IEnumerable<TagHelperDescriptor> components)
        {
            return RazorDiagnostic.Create(MultipleComponents, span ?? SourceSpan.Undefined, tagName, string.Join(", ", components.Select(c => c.DisplayName)));
        }

        public static readonly RazorDiagnosticDescriptor UnsupportedComplexContent = new RazorDiagnosticDescriptor(
            "BL9986",
            () => "Component attributes do not support complex content (mixed C# and markup). Attribute: '{0}', text '{1}'",
            RazorDiagnosticSeverity.Error);

        public static RazorDiagnostic Create_UnsupportedComplexContent(SourceSpan? source, TagHelperPropertyIntermediateNode node)
        {
            var content = string.Join("", node.Children.OfType<IntermediateToken>().Select(t => t.Content));
            return RazorDiagnostic.Create(UnsupportedComplexContent, source ?? SourceSpan.Undefined, node.AttributeName, content);
        }

        public static readonly RazorDiagnosticDescriptor UnboundComponentAttribute = new RazorDiagnosticDescriptor(
            "BL9987",
            () => "The component '{0}' does not have an attribute named '{1}'.",
            RazorDiagnosticSeverity.Error);

        public static RazorDiagnostic Create_UnboundComponentAttribute(SourceSpan? source, string componentType, TagHelperHtmlAttributeIntermediateNode node)
        {
            return RazorDiagnostic.Create(UnboundComponentAttribute, source ?? SourceSpan.Undefined, componentType, node.AttributeName);
        }

        private static SourceSpan? CalculateSourcePosition(
            SourceSpan? razorTokenPosition,
            TextPosition htmlNodePosition)
        {
            if (razorTokenPosition.HasValue)
            {
                var razorPos = razorTokenPosition.Value;
                return new SourceSpan(
                    razorPos.FilePath,
                    razorPos.AbsoluteIndex + htmlNodePosition.Position,
                    razorPos.LineIndex + htmlNodePosition.Line - 1,
                    htmlNodePosition.Line == 1
                        ? razorPos.CharacterIndex + htmlNodePosition.Column - 1
                        : htmlNodePosition.Column - 1,
                    length: 1);
            }
            else
            {
                return null;
            }
        }
    }
}
