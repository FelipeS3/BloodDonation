﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BloodDonation.Application.Models;

public class ResultViewModel
{
    public ResultViewModel(bool isSuccess = true, string message = "")
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

}
public class ResultViewModel<T> : ResultViewModel
{
    public ResultViewModel(T? data, bool isSuccess = true, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }

    public T? Data { get; private set; }

    public static ResultViewModel<T> Success(T data) => new(data);
    public static ResultViewModel<T> Error(string message) => new(default, false, message);
}