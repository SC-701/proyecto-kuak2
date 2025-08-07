using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class CategoriaDA : ICategoriaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public CategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearCategoria(CategoriaRequest categoria)
        {
            string sqlQuery = @"sp_Categoria_Crear";

            Guid idCategoria = Guid.NewGuid();

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idCategoria,
                nombre = categoria.Nombre,
                descripcion = categoria.Descripcion
            }, commandType: System.Data.CommandType.StoredProcedure);

            return resultadoQuery;
        }

        public async Task<IEnumerable<CategoriaResponse>> MostrarCategorias()
        {
            string sqlQuery = @"sp_Categoria_Mostrar";

            var resultado = await _sqlConnection.QueryAsync<CategoriaResponse>(sqlQuery, commandType: System.Data.CommandType.StoredProcedure);

            return resultado;
        }

        public async Task EditarCategoria(CategoriaRequest categoria)
        {
            string sqlQuery = @"sp_Categoria_Editar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idCategoria = categoria.IdCategoria,
                nombre = categoria.Nombre,
                descripcion = categoria.Descripcion
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task EliminarCategoria(Guid idCategoria)
        {
            string sqlQuery = @"sp_Categoria_Eliminar";

            await _sqlConnection.ExecuteAsync(sqlQuery, new
            {
                idCategoria
            }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}