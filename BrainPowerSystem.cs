using System;

namespace ElHerederoEinstein
{
    // Esta clase gestiona el recurso mental del jugador para resolver acertijos o usar habilidades
    public class BrainPowerSystem
    {
        // Propiedades de la mecánica
        public float MaxConcentration { get; private set; }
        public float CurrentConcentration { get; private set; }
        public float RegenerationRate { get; set; } // Puntos por segundo
        
        public bool IsExhausted => CurrentConcentration <= 0;

        // Eventos para la interfaz de usuario (UI)
        public event Action<float> OnConcentrationChanged;
        public event Action OnEurekaMoment; // Se activa al realizar una deducción exitosa

        public BrainPowerSystem(float maxConcentration = 100f, float regenerationRate = 5f)
        {
            MaxConcentration = maxConcentration;
            CurrentConcentration = maxConcentration;
            RegenerationRate = regenerationRate;
        }

        // Método para consumir concentración (al usar una pista o habilidad especial)
        public bool SpendConcentration(float amount)
        {
            if (CurrentConcentration >= amount)
            {
                CurrentConcentration -= amount;
                Console.WriteLine($"Concentración usada: {amount}. Nivel actual: {CurrentConcentration:F1}");
                
                OnConcentrationChanged?.Invoke(CurrentConcentration);
                return true;
            }

            Console.WriteLine("¡Fatiga mental! No tienes suficiente concentración para deducir esto.");
            return false;
        }

        // Simula el paso del tiempo para regenerar la mente (llamar en el Update del juego)
        public void UpdateRegeneration(float deltaTime)
        {
            if (CurrentConcentration < MaxConcentration)
            {
                CurrentConcentration += RegenerationRate * deltaTime;
                
                if (CurrentConcentration > MaxConcentration)
                    CurrentConcentration = MaxConcentration;

                OnConcentrationChanged?.Invoke(CurrentConcentration);
            }
        }

        // Habilidad especial: El Momento Eureka
        public void PerformEurekaDeduction()
        {
            float cost = 50f;
            if (SpendConcentration(cost))
            {
                Console.WriteLine("¡EUREKA! Has descubierto una conexión lógica inspirada en el Profesor.");
                OnEurekaMoment?.Invoke();
            }
        }
    }

    // Ejemplo de simulación de la lógica del juego
    class GameSimulation
    {
        static void Main()
        {
            Console.WriteLine("--- Iniciando El Heredero del Profesor Einstein ---");
            
            BrainPowerSystem playerMind = new BrainPowerSystem(100, 10);

            // Intentar una deducción costosa
            playerMind.PerformEurekaDeduction();

            // Simular el paso de 3 segundos de tiempo de juego
            Console.WriteLine("... El jugador analiza la situación (esperando regeneración) ...");
            playerMind.UpdateRegeneration(3.0f); 

            playerMind.PerformEurekaDeduction();
            playerMind.PerformEurekaDeduction(); // Esto podría fallar por falta de puntos
        }
    }
}
