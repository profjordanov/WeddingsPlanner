﻿using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Optional;

namespace WeddingsPlanner.Api.ModelBinders
{
    public class OptionModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.ModelType.GetTypeInfo().IsGenericType ||
                context.Metadata.ModelType.GetGenericTypeDefinition() != typeof(Option<>))
            {
                return null;
            }
            var types = context.Metadata.ModelType.GetGenericArguments();
            var obj = typeof(OptionModelBinder<>).MakeGenericType(types);

            return (IModelBinder)Activator.CreateInstance(obj);
        }
    }
}
