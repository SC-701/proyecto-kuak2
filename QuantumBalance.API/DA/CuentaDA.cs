using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class CuentaDA : ICuentaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public CuentaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearCuenta(Cuenta cuenta)
        {
            string sqlQuery = @"CrearCuenta";

            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sqlQuery,
                new
                {
                    idCuenta = Guid.NewGuid(),
                    nombreCuenta = cuenta.NombreCuenta,
                    descripcion = cuenta.Descripcion,
                    fechaCreacion = DateTime.Now,
                    activo = cuenta.Activo
                }
            );

            return resultadoQuery;
        }

        public async Task<Guid> ActualizarCuenta(Guid idCuenta, Cuenta cuenta)
        {
            string sqlQuery = @"ActualizarCuenta";
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sqlQuery,
                new
                {
                    idCuenta,
                    nombreCuenta = cuenta.NombreCuenta,
                    descripcion = cuenta.Descripcion,
                    fechaModificacion = DateTime.Now,
                    activo = cuenta.Activo
                }
               
            );
            return resultadoQuery;
        }

        public async Task<Guid> EliminarCuenta(Guid idCuenta)
        {
            await VerificarExistenciaCuenta(idCuenta);
            string sqlQuery = @"EliminarCuenta";
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sqlQuery,
                new
                {
                    //dudas aqui
                    id = idCuenta
                }
            );
            return resultadoQuery;
        }

        public async Task<Cuenta> ObtenerCuentaPorId(Guid idCuenta)
        {
            string sqlQuery = @"ObtenerCuenta";
            var resultadoQuery = await _sqlConnection.QueryAsync<Cuenta>(sqlQuery,
                new
                {
                    //dudas aqui
                    id = idCuenta
                }
            );

            return resultadoQuery.FirstOrDefault();
        }

        private async Task VerificarExistenciaCuenta(Guid idCuenta)
        {
            Cuenta? cuentaCheck = await ObtenerCuentaPorId(idCuenta);

            if (cuentaCheck == null)
            {
                throw new Exception("Esta cuenta no existe");

            }
        }

        public async Task<IEnumerable<Cuenta>> ObtenerCuentas()
        {
            string sqlQuery = @"ObtenerCuentas";
            var resultadoQuery = await _sqlConnection.QueryAsync<Cuenta>(sqlQuery);

            return resultadoQuery;
        }

        public async Task<Cuenta> DetalleCuenta(Guid idCuenta)
        {
            string sqlQuery = @"DetalleCuenta";
            var resultadoQuery = await _sqlConnection.QueryAsync<Cuenta>(sqlQuery,
                new
                {
                    //dudas aqui
                    id = idCuenta
                }
            );
            return resultadoQuery.FirstOrDefault();
        }

    }
}
