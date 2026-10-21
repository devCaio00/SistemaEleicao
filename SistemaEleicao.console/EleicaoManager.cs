using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEleicao
{
    public class EleicaoManager
    {
        private List<Candidato> _candidatos = new List<Candidato>();
        private HashSet<string> _eleitoresQueJaVotaram = new HashSet<string>();
        
        public int VotosBrancos { get; private set; } = 0;
        public int VotosNulos { get; private set; } = 0;

        // 1. Cadastrar Candidatos com validação de duplicidade
        public bool CadastrarCandidato(string nome, int numero)
        {
            if (_candidatos.Any(c => c.Numero == numero))
            {
                Console.WriteLine($"Erro: O número {numero} já está sendo utilizado por outro candidato.");
                return false;
            }

            _candidatos.Add(new Candidato(nome, numero));
            Console.WriteLine($"Candidato {nome} (Nº {numero}) cadastrado com sucesso!");
            return true;
        }

        // 2 e 3. Registrar o eleitor e processar o voto na mesma ação
        public bool Votar(string cpfOuTitulo, int numeroVoto)
        {
            // Valida se o eleitor já votou
            if (_eleitoresQueJaVotaram.Contains(cpfOuTitulo))
            {
                Console.WriteLine($"Erro: O eleitor portador do documento {cpfOuTitulo} já registrou seu voto.");
                return false;
            }

            // Registra que este eleitor votou
            _eleitoresQueJaVotaram.Add(cpfOuTitulo);

            // Regra para Voto em Branco (vamos considerar o número 0 para branco)
            if (numeroVoto == 0)
            {
                VotosBrancos++;
                Console.WriteLine("Voto em branco computado com sucesso.");
                return true;
            }

            // Busca o candidato pelo número
            var candidato = _candidatos.FirstOrDefault(c => c.Numero == numeroVoto);
            
            if (candidato != null)
            {
                candidato.AdicionarVoto();
                Console.WriteLine($"Voto computado para o candidato: {candidato.Nome}.");
            }
            else
            {
                // Se o número não for 0 e não pertencer a ninguém, é nulo
                VotosNulos++;
                Console.WriteLine("Número não encontrado. Voto nulo computado.");
            }

            return true;
        }

        // 4. Apuração de Votos e Vencedor
        public void ApurarVotos()
        {
            Console.WriteLine("\n--- RELATÓRIO FINAL DE APURAÇÃO ---");
            
            // Ordena a lista do mais votado para o menos votado
            var candidatosOrdenados = _candidatos.OrderByDescending(c => c.VotosRecebidos).ToList();
            int totalVotosValidos = 0;

            foreach (var candidato in candidatosOrdenados)
            {
                Console.WriteLine($"Candidato {candidato.Nome} ({candidato.Numero}): {candidato.VotosRecebidos} votos");
                totalVotosValidos += candidato.VotosRecebidos;
            }

            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Votos em Branco: {VotosBrancos}");
            Console.WriteLine($"Votos Nulos: {VotosNulos}");
            Console.WriteLine($"Total de Votos Realizados: {totalVotosValidos + VotosBrancos + VotosNulos}");
            Console.WriteLine("-----------------------------------");

            // Determinar o Vencedor
            if (candidatosOrdenados.Any() && candidatosOrdenados.First().VotosRecebidos > 0)
            {
                var vencedor = candidatosOrdenados.First();
                
                // Tratar um possível empate técnico
                var empatados = candidatosOrdenados.Where(c => c.VotosRecebidos == vencedor.VotosRecebidos).ToList();
                
                if (empatados.Count > 1)
                {
                    var nomesEmpatados = string.Join(" e ", empatados.Select(e => e.Nome));
                    Console.WriteLine($"RESULTADO: Empate técnico entre {nomesEmpatados} com {vencedor.VotosRecebidos} votos cada!");
                }
                else
                {
                    Console.WriteLine($"VENCEDOR: {vencedor.Nome} com {vencedor.VotosRecebidos} votos!");
                }
            }
            else
            {
                Console.WriteLine("RESULTADO: Nenhum candidato recebeu votos válidos nesta eleição.");
            }
        }
    }
}