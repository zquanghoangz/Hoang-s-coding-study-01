﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using FluentApiStudy.Utils;

namespace FluentApiStudy.PropertyChanged
{
    public static class NotifyPropertyChangedExtensions
    {
        public static PositivePropertyChangedExpectation<T>
            ShouldNotifyFor<T, TProp>(
                this T subject,
                Expression<Func<T, TProp>> propertyExpression)
            where T : INotifyPropertyChanged
        {
            return new PositivePropertyChangedExpectation<T>(subject, ExpressionUtilities.GetPropertyName(propertyExpression));
        }

        public static NegativePropertyChangedExpectation<T>
            ShouldNotNotifyFor<T, TProp>(
                this T subject,
                Expression<Func<T, TProp>> propertyExpression)
            where T : INotifyPropertyChanged
        {
            return new NegativePropertyChangedExpectation<T>(subject, ExpressionUtilities.GetPropertyName(propertyExpression));
        }
    }
}