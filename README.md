# Dungeon Cardler

[![Unity](https://img.shields.io/badge/Unity-2022.3%2B%20%7C%20Unity%206-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-9.0%2B-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Render Pipeline](https://img.shields.io/badge/URP-Universal%20Render%20Pipeline-4682B4?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/srp/Universal-Render-Pipeline)
[![Play on itch.io](https://img.shields.io/badge/Play%20on-itch.io-FA5C5C?style=for-the-badge&logo=itch.io&logoColor=white)](https://gurd62.itch.io/dungeon-cardler)

> A turn-based tactical deckbuilder roguelike built with **Unity** and **C#**, featuring event-driven architecture, modular ScriptableObject game systems, dynamic board-based enemy combat, item-card synergies, and encrypted save state persistence.

---

## Play the Game

> [!IMPORTANT]
> **Playable Web / Desktop Build Available on itch.io**
> 
> **[Click here to play Dungeon Cardler on itch.io](https://gurd62.itch.io/dungeon-cardler)**

---

## HR & Recruiter Overview

This project serves as a key piece of my software engineering portfolio. It demonstrates production-grade C# game development, modern Unity design patterns, scalable software architecture, and clean code principles.

### Key Developer Competencies Demonstrated
* **Object-Oriented & Modular System Architecture**: Built using decoupled systems (Card, Item, Enemy, Shop, Save, and UI systems) to maintain scalability, readability, and low coupling.
* **Event-Driven Programming**: Implemented C# delegates and events (`Action`, custom events) to eliminate direct class dependencies between UI elements, input handlers, and core game logic controllers.
* **Data-Driven Content Pipelines**: Utilized Unity ScriptableObjects for card actions, enemy behavior profiles, and item properties, enabling zero-code addition of new content.
* **Design Patterns**: Applied industry-standard patterns including **Strategy / Command** (Card Effect pipelines), **Observer** (Game state & UI synchronization), **Factory/Pool** (Round Buffers), and **Singleton** (Global Managers).
* **Data Security & Persistence**: Engineered custom file persistence featuring JSON serialization, cross-platform file path resolution (`Path.Combine`), and XOR payload encryption/decryption.
* **Input Abstraction**: Fully decoupled input reading from gameplay logic using Unity's new Input System, supporting multiple action maps (Player, Shop, Reward, UI).

### Quick Summary Table for Hiring Managers

| Category | Implementation Highlights |
| :--- | :--- |
| **Engine & Language** | Unity 6 / URP, C# (.NET Scripting) |
| **Architecture** | Event-Driven / Observer Pattern, Strategy Pattern for Effects |
| **Data & Security** | Custom JSON Save System with XOR Data Encryption |
| **Input System** | Unity Input System Package (Action Maps, Context Switching) |
| **UI Management** | Dynamic Canvas UI managed reactively via Game Events |
| **Content Scalability** | ScriptableObjects for modular Cards, Items, and Enemies |

---

## Game Overview & Core Mechanics

**Dungeon Cardler** combines classic tactical deckbuilding with roguelike wave survival and item synergy mechanics. Players navigate tactical encounters by playing cards, utilizing equipment items, and adapting to wave-based enemy behaviors.

### Core Systems
1. **Turn-Based Board Combat**:
   * Waves with varied enemy archetypes (Animals, Knights, Spikey Monsters, Bosses).
   * Wave management with procedural horde scaling and periodic boss encounters (e.g., Slime King every 10 rounds).
2. **Modular Card System**:
   * Cards execute dynamic effects based on conditional triggers (e.g., equipped item conditions, attack types).
   * Card unlock progression integrated into item acquisition and combat victory.
3. **Item & Inventory Synergy**:
   * Items equipable in inventory slots alter card outcomes and boost player combat stats.
   * Consumables (potions) and gear (weapons/armor) dynamically manipulate health, mana, and coin resources.
4. **Shop & Reward Interstitials**:
   * Between wave transitions, players enter procedural buffer states (Shop or Reward screens) managed by a weighted buffer pool.
5. **Encrypted Save & Load System**:
   * Automatic progress serialization via `FileDataHandler` with XOR payload obfuscation to prevent state tampering across sessions.

---

## Technical Architecture & Design Patterns

```
                          ┌───────────────────────────┐
                          │       GameManager         │
                          │ (Central Coordinator)     │
                          └─────────────┬─────────────┘
                                        │
           ┌────────────────────────────┼────────────────────────────┐
           ▼                            ▼                            ▼
┌────────────────────┐       ┌────────────────────┐       ┌────────────────────┐
│   PlayerManager    │       │     EnemyBoard     │       │    UIManager       │
│  (Stats, Cards,    │       │  (Horde Logic,     │       │ (Reactive HUD &    │
│   Inventory)       │       │   Enemy Entities)  │       │  Prompts)          │
└──────────┬─────────┘       └──────────┬─────────┘       └──────────┬─────────┘
           │                            │                            │
           └────────────────────────────┼────────────────────────────┘
                                        │ (C# Events / Delegates)
                                        ▼
                          ┌───────────────────────────┐
                          │    RoundManager & Pool    │
                          │ (State & Phase Handling)  │
                          └───────────────────────────┘
```

### Architectural Highlights

#### 1. Decoupled Event-Driven Pipeline
The project enforces strict separation of concerns. `GameManager` and UI scripts subscribe to C# events emitted by game entities rather than polling states every frame.
```csharp
// Example event subscriptions in GameManager
_player.OnPlayerTurnEnded += PlayerTurnEnd;
_player.OnCardUse += PlayerCardUse;
_enemyBoard.OnBoardClear += BoardClear;
_enemyBoard.OnEnemyKilled += HandleCoinsGain;
```

#### 2. Polymorphic Card & Effect Strategy
Cards execute modular behaviors via abstract `CardEffect` ScriptableObjects and conditional wrappers (`CardConditionalWrapper`), making new card creation completely data-driven:
```csharp
[CreateAssetMenu(menuName = "Cards/New Card")]
public class CardBase : ScriptableObject
{
    [SerializeField] private CardConditionalWrapper _mainEffect;
    [SerializeField] private CardConditionalWrapper _secondaryEffect;

    public void Play(CardContext context)
    {
        if (_mainEffect.ItemConditionMet(context))
            _mainEffect.effect.Execute(context);
        else if (_secondaryEffect.AttackTypeConditionMet(context))
            _secondaryEffect.effect.Execute(context);
    }
}
```

#### 3. Secure File Data Handler & Save System
Game state is saved via `DataPersistenceManager` utilizing an interface contract (`IDataPersistence`) and encrypted file streaming:
```csharp
public class FileDataHandler
{
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ _encryptionCodeWord[i % _encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
```

---

## Codebase Structure

```
Assets/Scripts/
├── Board/             # Enemy board state & horde spawning algorithms (EnemyBoard, HordeLogic)
├── Card/              # Card data, deck manipulation, conditional effects & unlock system
│   ├── CardEffects/   # ScriptableObject instances for specific card abilities
│   └── CardSystem/    # CardBase, CardEffect strategy contracts, CardController
├── Database/          # Runtime databases for enemies and items
├── Enemy/             # Enemy inheritance hierarchy, AI controllers & abilities
├── General/           # GameManager, RoundManager, Audio, Settings & GameInput abstraction
├── Item/              # Inventory system, weapon/potion classes & item effects
├── MainMenu/          # Main menu controllers & scene loader
├── Player/            # Player stats, targeting, inventory, combat & card stash managers
├── SaveSystem/        # DataPersistenceManager, FileDataHandler & XOR encryption
├── ShopSystem/        # Shop logic, UI integration & transaction handling
└── UI/                # Reactive UI controllers (Health, Mana, Items, Cards, Enemy status)
```

---

## Tech Stack & Dependencies

* **Game Engine**: Unity (Universal Render Pipeline - URP)
* **Programming Language**: C# (.NET Framework / Mono)
* **Packages & Frameworks**:
  * `com.unity.inputsystem` (New Unity Input System with action maps)
  * `com.unity.render-pipelines.universal` (Universal Render Pipeline 2D)
  * `com.unity.ugui` (TextMeshPro & Unity Canvas UI)
  * `com.unity.2d.aseprite`, `com.unity.2d.animation` & `DOTween by DEMIGIANT` (2D Animation & Asset Pipeline)