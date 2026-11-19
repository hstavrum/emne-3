namespace _321A
{
    // Class responsible for storing and managing character counts
    internal class CharCounter
    {
        // A private list to store CharCount objects (each representing a unique character and its count)
        private readonly List<CharCount> _characterCounts = new List<CharCount>();

        /*internal void AddText(string text)
        {
            // Use the null-coalescing operator to ensure we don't crash if 'text' is null
            foreach (var character in text ?? string.Empty)
            {
                // Check if this character has already been recorded in our list using LINQ's FirstOrDefault method
                var existingCount = _characterCounts.FirstOrDefault(c => c.Character == character);

                if (existingCount != null)
                {
                    existingCount.Count++;
                }
                else
                {
                    _characterCounts.Add(new CharCount { Character = character, Count = 1 });
                }
            }
        } */

        // instead use a foreach loop to iterate through each character in the input text and update the count accordingly
        // Processes a string and updates the internal counts for every character found
        internal void AddText(string text)
        {
            // Use the null-coalescing operator to ensure we don't crash if 'text' is null
            foreach (var character in text ?? string.Empty)
            {
                // --- START OF OPTION 1: MANUAL SEARCH ---
                // Instead of using LINQ, we manually look through the list
                CharCount existingCount = null;

                foreach (var count in _characterCounts)
                {
                    if (count.Character == character)
                    {
                        // We found the character! Store it in our variable and stop the loop.
                        existingCount = count;
                        break;
                    }
                }
                // --- END OF OPTION 1 ---

                if (existingCount != null)
                {
                    // If the loop found a match, increment that specific object's count
                    existingCount.Count++;
                }
                else
                {
                    // If the loop finished and existingCount is still null, it's a new character
                    _characterCounts.Add(new CharCount { Character = character, Count = 1 });
                }
            }
        }

        internal void ShowCounts()
        {
            foreach (var count in _characterCounts)
            {
                Console.WriteLine(count.Character + " - " + count.Count);
            }
        }

        // A simple data model to hold a character and its corresponding frequency
        internal class CharCount
        {
            public char Character { get; set; }  // The character being counted
            public int Count { get; set; }       // How many times it has appeared
        }
    }
}

// instead use a Dictionary to store the character counts for more efficient lookups and updates
/*  private readonly Dictionary<char, int> _characterCounts = new Dictionary<char, int>();

  internal void AddText(string text)
  {
      foreach (var character in text ?? string.Empty)
      {
          if (_characterCounts.ContainsKey(character))
          {
              // Update the count if it exists
              _characterCounts[character]++;
          }
          else
          {
              // Add it if it's new
              _characterCounts[character] = 1;
          }
      }
  }

  internal void ShowCounts()
  {
      foreach (var kvp in _characterCounts)
      {
          // kvp.Key is the character, kvp.Value is the count
          Console.WriteLine(kvp.Key + " - " + kvp.Value);
      }
  }*/
