﻿namespace delphibackend.IAM.Domain.Model.Commands;

public record SignInCommand(string Email, string Password);