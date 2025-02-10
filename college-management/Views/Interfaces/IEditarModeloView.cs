using college_management.Dados.Modelos;


namespace college_management.Views.Interfaces;


public interface IEditarModeloView<out T> where T : Modelo
{
	public T Editar();
}
