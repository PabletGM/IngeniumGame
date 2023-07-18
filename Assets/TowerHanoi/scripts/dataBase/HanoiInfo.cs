
public class HanoiInfo
{
    //variables

    //tiempo del jugador que tarda en ganar
    public int tiempoPlayer;
    //numero de jugadas
    public int numJugadas;
    //numero de movimientos incorrectos
    public int numMovimientosIncorrectos;
    //numero de movimientos OutOfLimits
    public int numMovimientosOutOflimits;

    //constructor
    public HanoiInfo(int tiempoPlayer,int numJugadas, int numMovimientosIncorrectos, int numMovimientosOutOflimits)
    {
        this.tiempoPlayer = tiempoPlayer;
        this.numJugadas = numJugadas;
        this.numMovimientosIncorrectos = numMovimientosIncorrectos;
        this.numMovimientosOutOflimits = numMovimientosOutOflimits;
    }
}
