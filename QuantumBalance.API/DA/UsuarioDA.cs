using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using System.Data.SqlClient;

public class UsuarioDA : IUsuarioDA
{
    private readonly IRepositorioDapper _repositorioDapper;
    private readonly SqlConnection _sqlConnection;

    public UsuarioDA(IRepositorioDapper repositorioDapper)
    {
        _repositorioDapper = repositorioDapper;
        _sqlConnection = _repositorioDapper.ObtenerConexion();
    }

    public async Task<IEnumerable<UsuarioResponse>> ObtenerTodosLosUsuarios()
    {
        string sqlQuery = @"sp_Usuario_Mostrar";
        var resultado = await _sqlConnection.QueryAsync<UsuarioResponse>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);
        return resultado;
    }

    public async Task<UsuarioResponse> ObtenerUsuarioPorId(Guid idUsuario)
    {
        string sqlQuery = @"sp_Usuario_ObtenerPorId";
        var resultado = await _sqlConnection.QueryFirstOrDefaultAsync<UsuarioResponse>(sqlQuery, new { idUsuario }, commandType: System.Data.CommandType.StoredProcedure);
        return resultado;
    }

    public async Task<Guid> CrearUsuario(UsuarioRequest usuario)
    {
        string sqlQuery = @"sp_Usuario_Crear";
        Guid idUsuario = Guid.NewGuid();
        Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
        {
            idUsuario,
            nombre = usuario.Nombre,
            primerApellido = usuario.PrimerApellido,
            segundoApellido = usuario.SegundoApellido,
            correo = usuario.Correo,
            contrasena = usuario.Contrasena
        }, commandType: System.Data.CommandType.StoredProcedure);

        return resultadoQuery;
    }

    public async Task<Guid> EditarUsuario(UsuarioRequest usuario)
    {
        string sqlQuery = @"sp_Usuario_Editar";

        await _sqlConnection.ExecuteAsync(sqlQuery, new
        {
            idUsuario = usuario.IdUsuario,
            nombre = usuario.Nombre,
            primerApellido = usuario.PrimerApellido,
            segundoApellido = usuario.SegundoApellido,
            correo = usuario.Correo,
            contrasena = usuario.Contrasena
        }, commandType: System.Data.CommandType.StoredProcedure);

        return usuario.IdUsuario;
    }

    public async Task<Guid> EliminarUsuario(Guid idUsuario)
    {
        string sqlQuery = @"sp_Usuario_Eliminar";
        var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new { idUsuario }, commandType: System.Data.CommandType.StoredProcedure);
        return resultado;
    }
}
