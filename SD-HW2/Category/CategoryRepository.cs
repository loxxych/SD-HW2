namespace SD_HW2.Category;

public class CategoryRepository
{
    private ICategoryFactory _categoryFactory;
    
    public List<Category> Categories { get; }
    
    public List<String> CategoriesNames
    {
        get
        {
            List<String> categoriesNames = [];

            foreach (var category in Categories)
            {
                categoriesNames.Add(category.Name);
            }

            return categoriesNames;
        }
    }

    public List<String> CategoriesByType(string typeStr)
    {
        List<String> categoriesNames = [];

        var type = typeStr == "Расход" ? Type.Withdrawal : Type.Deposit;
        
        foreach (var category in Categories)
        {
            if (category.Type == type)
            {
                categoriesNames.Add(category.Name);
            }
        }
        
        return categoriesNames;
    }

    public CategoryRepository(ICategoryFactory categoryFactory)
    {
        _categoryFactory = categoryFactory;
        Categories = [];
    }
    
    public void AddCategory(Type type, string name)
    {
        var category = _categoryFactory.createCategory(type, name);
        Categories.Add(category);
    }

    public void RemoveCategory(string name)
    {
        bool found = false;
        foreach (var category in Categories)
        {
            if (category.Name == name)
            {
                Categories.Remove(category);
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new ArgumentOutOfRangeException($"Не существует категории с именем {name} ");
        }
    }
    
    public Category FindCategory(string name)
    {
        foreach (var category in Categories) 
        {
            if (category.Name == name)
            {
                return category;
            }
        }
        throw new ArgumentOutOfRangeException($"Не существует категории с именем {name}");
    }
    
    public void ChangeCategoryName(string name, string newName)
    {
        var category = FindCategory(name);
        category.Name = newName;
    }
    
    public void ChangeCategoryType(string name, Type newType)
    {
        var category = FindCategory(name);
        category.Type = newType;
    }
}