namespace RestApiApp;

public interface IMockDb
{
    public ICollection<Animal> GetAllAnimals();
    public Animal? GetAnimalDetails(int id);
    public bool AddAnimal(Animal animal);
    public Animal? RemoveAnimal(int id);
    public Animal? ReplaceAnimal(int id, Animal animal);

}

public class MockDb : IMockDb
{
    private ICollection<Animal> _animals;

    public MockDb()
    {
        _animals = new List<Animal>
        {
            new Animal
            {
                Id = 1,
                Name = "Puszek",
                Category = "Doggo",
                Weight = 10.5,
                Color = "Grey"
            },
            new Animal
            {
                Id = 2,
                Name = "Pusia",
                Category = "Cateł",
                Weight = 3,
                Color = "Black"
            }
        };
    }

    public ICollection<Animal> GetAllAnimals()
    {
        return _animals;
    }

    public Animal? GetAnimalDetails(int id)
    {
        return _animals.FirstOrDefault(e => e.Id == id);
    }

    public bool AddAnimal(Animal animal)
    {
        _animals.Add(animal);
        return true;
    }

    public Animal? RemoveAnimal(int id)
    {
        var animalToDelete = _animals.FirstOrDefault(animal => animal.Id == id);
        if (animalToDelete is null) return null;
        _animals.Remove(animalToDelete);
        return animalToDelete;
    }

    public Animal? ReplaceAnimal(int id, Animal animal)
    {
        var animalToDelete = _animals.FirstOrDefault(e => e.Id == id);
        if (animalToDelete is not null)
        {
            _animals.Remove(animalToDelete);
        }

        _animals.Add(animal);
        return animal;
    }
}