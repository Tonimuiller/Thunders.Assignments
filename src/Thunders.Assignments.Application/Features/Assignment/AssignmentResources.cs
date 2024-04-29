namespace Thunders.Assignments.Application.Features.Assignment;

public static class AssignmentResources
{
    public static string TitleIsRequired = "O título da tarefa é obrigatório.";
    public static string ScheduleIsRequired = "A data da tarefa é obrigatória.";
    public static string AssignmentIdNotFound = "Não foi possível encontrar a tarefa com o identificador informado.";
    public static string CompleteScheduleDateRangeIsRequired = 
        "Não é possível filtrar somente pela data inicial ou somente pela data final de agendamento.";
    public static string InvalidDateRange = "A data inicial deve ser menor ou igual à data final.";
}
