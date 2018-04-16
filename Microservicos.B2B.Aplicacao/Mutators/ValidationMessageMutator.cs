using NServiceBus.MessageMutator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using System.Reflection;

namespace Microservicos.B2B.Mutators
{
    public class ValidationMessageMutator : IMutateIncomingMessages
    {
        public Task MutateIncoming(MutateIncomingMessageContext context)
        {
            ValidateDataAnnotations(context.Message);
            return Task.CompletedTask;
        }

        static void ValidateDataAnnotations(object message)
        {
            var context = new ValidationContext(message, null, null);

            
            var vt = typeof(AbstractValidator<>);
            var et = message.GetType();
            var evt = vt.MakeGenericType(et);

            var validatorType = FindValidatorType(Assembly.GetExecutingAssembly(), evt);

            var validatorInstance = (IValidator)Activator.CreateInstance(validatorType);

            var validationResult = validatorInstance.Validate(message);

            if (validationResult.IsValid)
            {
                //log.Info($"Validation succeeded for message: {message}");
                return;
            }

            var errorMessage = new StringBuilder();
            var error = $"Validation failed for message {message}, with the following error/s:";
            errorMessage.AppendLine(error);

            //foreach (var vr in validationResult.)
            //{
            //    errorMessage.AppendLine(vr.ErrorMessage);
            //}

            //log.Error(errorMessage.ToString());
            throw new Exception(errorMessage.ToString());
        }

        public static Type FindValidatorType(Assembly assembly, Type evt)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (evt == null) throw new ArgumentNullException("evt");
            return assembly.GetTypes().FirstOrDefault(t => t.IsSubclassOf(evt));
        }
    }
}
