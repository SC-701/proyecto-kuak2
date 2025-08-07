using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class MovimientoFlujo : IMovimientoFlujo
    {
        private readonly IMovimientoDA _movimientoDA;

        public MovimientoFlujo(IMovimientoDA movimientoDA)
        {
            _movimientoDA = movimientoDA;
        }

        public async Task<IEnumerable<MovimientoResponse>> ObtenerTodosLosMovimientos()
        {
            return await _movimientoDA.ObtenerTodosLosMovimientos();
        }

        public async Task<MovimientoResponse?> ObtenerMovimientoPorId(Guid idMovimiento)
        {
            return await _movimientoDA.ObtenerMovimientoPorId(idMovimiento); 
        }


        public async Task<Guid> CrearMovimiento(MovimientoRequest movimiento)
        {
            return await _movimientoDA.CrearMovimiento(movimiento);
        }

        public async Task<Guid> EditarMovimiento(MovimientoRequest movimiento)
        {
            return await _movimientoDA.EditarMovimiento(movimiento);
        }

        public async Task<Guid> EliminarMovimiento(Guid idMovimiento)
        {
            return await _movimientoDA.EliminarMovimiento(idMovimiento);
        }
    }
}
