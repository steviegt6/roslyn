﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports Microsoft.CodeAnalysis.CodeGeneration
Imports Microsoft.CodeAnalysis.Editing

Namespace Microsoft.CodeAnalysis.VisualBasic.CodeGeneration
    Friend NotInheritable Class VisualBasicFlagsEnumGenerator
        Inherits AbstractFlagsEnumGenerator

        Public Shared ReadOnly Instance As New VisualBasicFlagsEnumGenerator

        Private Sub New()
        End Sub

        Protected Overrides ReadOnly Property SyntaxGenerator As SyntaxGeneratorInternal
            Get
                Return VisualBasicSyntaxGeneratorInternal.Instance
            End Get
        End Property

        Protected Overrides Function CreateExplicitlyCastedLiteralValue(
                enumType As INamedTypeSymbol,
                underlyingSpecialType As SpecialType,
                constantValue As Object) As SyntaxNode
            Dim expression = GenerateNonEnumValueExpression(
                enumType.EnumUnderlyingType, constantValue, canUseFieldReference:=True)
            Dim constantValueULong = underlyingSpecialType.ConvertUnderlyingValueToUInt64(constantValue)
            If constantValueULong = 0 Then
                Return expression
            End If

            Return Me.SyntaxGenerator.ConvertExpression(enumType, expression)
        End Function

        Protected Overrides Function IsValidName(enumType As INamedTypeSymbol, name As String) As Boolean
            If name = "_" Then
                Return False
            End If

            If Not SyntaxFacts.IsValidIdentifier(name) Then
                Return False
            End If

            Return enumType.GetMembers(name).Length = 1
        End Function
    End Class
End Namespace
