using Backend.Models;
using Xunit;

namespace backend.tests;

public class UnitTest1
{
    [Fact]
    public void NewTask_ShouldNotBeCompleted()
    {
        var task = new CloudTask();
        task.Name = "Przetestować bezpiecznik";

        Assert.False(task.IsCompleted);
    }
}