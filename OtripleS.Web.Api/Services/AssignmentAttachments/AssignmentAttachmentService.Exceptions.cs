﻿//---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
//----------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OtripleS.Web.Api.Models.AssignmentAttachments;
using OtripleS.Web.Api.Models.AssignmentAttachments.Exceptions;

namespace OtripleS.Web.Api.Services.AssignmentAttachments
{
    public partial class AssignmentAttachmentService
    {
        private delegate ValueTask<AssignmentAttachment> ReturningAssignmentAttachmentFunction();

        private async ValueTask<AssignmentAttachment> TryCatch(
            ReturningAssignmentAttachmentFunction returningAssignmentAttachmentFunction)
        {
            try
            {
                return await returningAssignmentAttachmentFunction();
            }
            catch (NullAssignmentAttachmentException nullAssignmentAttachmentInputException)
            {
                throw CreateAndLogValidationException(nullAssignmentAttachmentInputException);
            }
        }

        private AssignmentAttachmentValidationException CreateAndLogValidationException(Exception exception)
        {
            var AssignmentAttachmentValidationException = new AssignmentAttachmentValidationException(exception);
            this.loggingBroker.LogError(AssignmentAttachmentValidationException);

            return AssignmentAttachmentValidationException;
        }
    }
}
