using Xunit;
using SistemaEleicao;

namespace SistemaEleicao.Tests;

public class ManagerTests
{
    [Fact]
    public void CadastrarCandidato_ComNumeroInedito_DeveRetornarTrue()
    {
        // Arrange
        var manager = new EleicaoManager();

        // Act
        var resultado = manager.CadastrarCandidato("Candidato 1", 10);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_DeveRetornarFalse()
    {
        // Arrange
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Candidato 1", 10); // Cadastra o primeiro com o número 10

        // Act
        var resultadoDuplicado = manager.CadastrarCandidato("Candidato 2", 10); // Tenta duplicar o número 10

        // Assert
        Assert.False(resultadoDuplicado);
    }

    [Fact]
    public void Votar_ComEleitorDuplicado_DeveRetornarFalseEBloquearVoto()
    {
        // Arrange
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Candidato 1", 10);

        // Primeiro voto do eleitor
        manager.Votar("123.456.789-00", 10);

        // Act
        // O mesmo eleitor tenta votar novamente
        var segundoVoto = manager.Votar("123.456.789-00", 10);

        // Assert
        Assert.False(segundoVoto);
    }

    [Fact]
    public void Votar_EmCandidatoInexistente_DeveContabilizarComoVotoNulo()
    {
        // Arrange
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Candidato 1", 10);

        // Act
        // Eleitor vota no número 99, que não foi cadastrado
        manager.Votar("111.222.333-44", 99);

        // Assert
        Assert.Equal(1, manager.VotosNulos);
    }

    [Fact]
    public void Votar_EmBranco_DeveContabilizarComoVotoBranco()
    {
        // Arrange
        var manager = new EleicaoManager();

        // Act
        // Eleitor vota 0 (regra que definimos para branco)
        manager.Votar("999.888.777-66", 0);

        // Assert
        Assert.Equal(1, manager.VotosBrancos);
    }
}