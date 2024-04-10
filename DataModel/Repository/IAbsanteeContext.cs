namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

using DataModel.Model;

public interface IAbsanteeContext
{
	DbSet<ColaboratorDataModel> Colaboradores { get; set; }
}