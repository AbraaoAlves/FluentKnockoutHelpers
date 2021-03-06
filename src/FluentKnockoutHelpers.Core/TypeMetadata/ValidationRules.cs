﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FluentKnockoutHelpers.Core.TypeMetadata
{
    public abstract class ValidationRule
    {
        protected ValidationRule(string name, ValidationAttribute attribute) :
            this(name, attribute.ErrorMessage)
        {
        }

        protected ValidationRule(string name, string errorMessage)
        {
            Name = name;
            ErrorMessage = errorMessage;
        }

        protected ValidationRule(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public string ErrorMessage { get; set; }
    }

    public class RequiredValidationRule : ValidationRule
    {
        public RequiredValidationRule(RequiredAttribute attribute) :
            base("Required", attribute)
        {
        }
    }

    public class RangeValidationRule : ValidationRule
    {
        public RangeValidationRule(RangeAttribute attribute) :
            base("Range", attribute)
        {
            Maxmium = attribute.Maximum;
            Minimum = attribute.Minimum;
        }

        public object Maxmium { get; private set; }
        public object Minimum { get; private set; }
    }

    public class MinLengthValidationRule : ValidationRule
    {
        public MinLengthValidationRule(MinLengthAttribute attribute) :
            base("MinLength", attribute)
        {
            Length = attribute.Length;
        }

        public MinLengthValidationRule(StringLengthAttribute attribute) :
            base("MinLength", attribute)
        {
            Length = attribute.MinimumLength;
        }

        public int Length { get; private set; }
    }

    public class MaxLengthValidationRule : ValidationRule
    {
        public MaxLengthValidationRule(MaxLengthAttribute attribute) :
            base("MaxLength", attribute)
        {
            Length = attribute.Length;
        }

        public MaxLengthValidationRule(StringLengthAttribute attribute) :
            base("MaxLength", attribute)
        {
            Length = attribute.MaximumLength;
        }

        public int Length { get; private set; }
    }

    public class RegexValidationRule : ValidationRule
    {
        public RegexValidationRule(RegularExpressionAttribute attribute) :
            base("Regex", attribute)
        {
            Pattern = attribute.Pattern;
        }

        public string Pattern { get; set; }
    }

    public class EmailAddressValidationRule : ValidationRule
    {
        public EmailAddressValidationRule(EmailAddressAttribute attribute) :
            base("EmailAddress", attribute)
        {
        }
    }

    public class CompareValidationRule : ValidationRule
    {
        public CompareValidationRule(CompareAttribute attribute) :
            base("Compare", attribute)
        {
            OtherField = attribute.OtherProperty;
        }

        public string OtherField { get; private set; }
    }

    public class PhoneValidationRule : ValidationRule
    {
        public PhoneValidationRule(PhoneAttribute attribute) :
            base("Phone", attribute)
        {
        }
    }

    public class UrlValidationRule : ValidationRule
    {
        public UrlValidationRule(UrlAttribute attribute) :
            base("Url", attribute)
        {
        }
    }
}
