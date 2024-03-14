using System;
using System.Collections.Generic;

public class Usuario {
    public int Id { get; }
    public string Nome { get; }
    public List<Usuario> Amigos { get; }

    public Usuario(int id, string nome) {
        Id = id;
        Nome = nome;
        Amigos = new List<Usuario>();
    }

    public void AdicionarAmigo(Usuario amigo) {
        if (!Amigos.Contains(amigo)) {
            Amigos.Add(amigo);
            amigo.Amigos.Add(this); 
        }
    }

    public override string ToString() {
        return $"{Id}: {Nome}";
    }
}

public class RedeSocial {
    private List<Usuario> usuarios;

    public RedeSocial() {
        usuarios = new List<Usuario>();
    }

    public void AdicionarUsuario(Usuario usuario) {
        usuarios.Add(usuario);
    }

    public void AdicionarConexao(int idUsuario1, int idUsuario2) {
        Usuario usuario1 = ProcurarUsuarioPorId(idUsuario1);
        Usuario usuario2 = ProcurarUsuarioPorId(idUsuario2);

        if (usuario1 != null && usuario2 != null && usuario1 != usuario2) {
            usuario1.AdicionarAmigo(usuario2);
        }
    }

    public bool VerificarConexao(int idUsuario1, int idUsuario2) {
        Usuario usuario1 = ProcurarUsuarioPorId(idUsuario1);
        Usuario usuario2 = ProcurarUsuarioPorId(idUsuario2);

        if (usuario1 != null && usuario2 != null) {
            return usuario1.Amigos.Contains(usuario2);
        }

        return false;
    }

    private Usuario ProcurarUsuarioPorId(int id) {
        foreach (Usuario usuario in usuarios) {
            if (usuario.Id == id) {
                return usuario;
            }
        }
        return null;
    }

    public void AdicionarConexoesFalsas() {
        // conexões falsas
        AdicionarConexao(2, 2); 
        AdicionarConexao(1, 4); 
    }
}

public class Program {
    public static void Main(string[] args) {
        RedeSocial redeSocial = new RedeSocial();

        Usuario usuario1 = new Usuario(1, "Alice");
        Usuario usuario2 = new Usuario(2, "Bob");
        Usuario usuario3 = new Usuario(3, "Carol");

        redeSocial.AdicionarUsuario(usuario1);
        redeSocial.AdicionarUsuario(usuario2);
        redeSocial.AdicionarUsuario(usuario3);

        // conexões verdadeiras
        redeSocial.AdicionarConexao(1, 2);
        redeSocial.AdicionarConexao(1, 3);
        redeSocial.AdicionarConexao(2, 3);

        //conexões falsas
        redeSocial.AdicionarConexoesFalsas();

        Console.WriteLine("Verificando conexões:");
        Console.WriteLine($"Alice e Bob são amigos: {redeSocial.VerificarConexao(1, 2)}");
        Console.WriteLine($"Alice e Carol são amigos: {redeSocial.VerificarConexao(1, 3)}");
        Console.WriteLine($"Bob e Carol são amigos: {redeSocial.VerificarConexao(2, 3)}");
        Console.WriteLine($"Bob e Alice são amigos: {redeSocial.VerificarConexao(2, 1)}");
        Console.WriteLine($"Bob e ele mesmo são amigos: {redeSocial.VerificarConexao(2, 2)}");
        Console.WriteLine($"Alice e um usuário inexistente são amigos: {redeSocial.VerificarConexao(1, 4)}");

        // se alterarmos a Id a sentança vira falsa = Console.WriteLine($"Alice e Bob são amigos: {redeSocial.VerificarConexao(1, 6)}"); == false
    }
}

