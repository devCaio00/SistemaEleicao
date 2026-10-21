using System;

namespace SistemaEleicao
{
    public class Candidato
    {
        public string Nome { get; private set; }
        public int Numero { get; private set; }
        public int VotosRecebidos { get; private set; }

        public Candidato(string nome, int numero)
        {
            Nome = nome;
            Numero = numero;
            VotosRecebidos = 0;
        }

        public void AdicionarVoto()
        {
            VotosRecebidos++;
        }
    }
}