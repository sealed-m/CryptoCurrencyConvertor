﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application.Contract.Responses
{
    public class ValidateableResponse
    {
        private readonly IList<string> _errorMessages;

        public ValidateableResponse(IList<string> errors = null)
        {
            _errorMessages = errors ?? new List<string>();
        }

        public bool HasError => _errorMessages.Any();

        public IReadOnlyCollection<string> Errors => new ReadOnlyCollection<string>(_errorMessages);
    }

    public class ValidateableResponse<TModel> : ValidateableResponse
        where TModel : class
    {

        public ValidateableResponse(TModel model, IList<string> validationErrors = null)
            : base(validationErrors)
        {
            Result = model;
        }

        public TModel Result { get; }
    }
}
