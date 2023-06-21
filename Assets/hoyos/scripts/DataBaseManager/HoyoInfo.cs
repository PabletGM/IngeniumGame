
public class HoyoInfo
{
    //variables

    //nombre del jugador
    public string namePlayer;
    //correo electronico
    public string mailPlayer;
    //numero de toques totales
    public int numberTicksTotal;
    //tiempo total
    public int totalTime;
    //array de numero de toques de cada hoyo
    public int[] numPicadasCadaHoyo;

    //constructor
    public HoyoInfo(string namePlayer,string mailPlayer, int numberTicksTotal,int  totalTime, int[]numPicadasCadaHoyo)
    {
        this.namePlayer = namePlayer;
        this.mailPlayer = mailPlayer;
        this.numberTicksTotal = numberTicksTotal;
        this.totalTime = totalTime;
        this.numPicadasCadaHoyo = numPicadasCadaHoyo;
    }
}
