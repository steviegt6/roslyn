# Unsafe Flags

## Summary

This feature provides finer control over what behavior is allowed within unsafe contexts and is a preliminary step toward allowing users to knowingly ignore access modifiers within their code.

## Motivation

The idea of an "unsafe context" as it exists officially in C# is mostly restricted to unmanaged pointers and some other "unsafe" operations like using the `sizeof` operator in non-constant contexts. This allowance may not necessarily be desired, especially because a lot of these behaviors aren't directly related. This feature also allows for us to add more flags in the future for different behaviors, such as "unsafe accessors" allowing users to safely and knowingly bypass access modifiers in rare situations without any overhead.

## Language specification

Unsafe flags extend the existing unsafe modifier and unsafe statement syntaxes.

```antlr
unsafe_modifier
    : 'unsafe' ('[' unsafe_flags ']')?
    ;

unsafe_statement
    : 'unsafe' ('[' unsafe_flags ']')?
    ;

unsafe_flags_list
    : unsafe_flag (',' unsafe_flag)*

unsafe_flag
    : 'sizeof'
    | 'pointer'
    ;
```
