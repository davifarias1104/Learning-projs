/// 3035425
let inv = {
  resources: {
    wood: 10,
    stone: 20
  },
  buildings: {},
  items: {}
};

const Fire = {
  action: {
    lightFire: {
      name: 'Light Fire',
      buildMsg: 'You lit the Fire',
      cost: () => ({ wood: 10 }),
      get: () => ({})
    },

    getWood: {
      name: 'Get Wood',
      buildMsg: 'You gathered leftover branches',
      cost: () => ({}),
      get: () => ({ wood: 1 })
    },

    gatherStones: {
      name: 'Gather Stones',
      buildMsg: 'You gathered scattered stones',
      cost: () => ({}),
      get: () => ({ stone: 1 })
    }
  },

  building: {
    craftingTable: {
      name: 'Crafting Table',
      maximum: 1,
      availableMsg: 'You unlocked new recipes',
      buildMsg: 'You made a Crafting Table',
      requirements: () => ({ wood: 30 }),
      cost: () => ({ wood: 30 }),
      get: () => ({ craftingTable: 1 })
    }
  }
};


// ===============================
// Executa ações e construções
// ===============================
function checkAction(obj) {
  const cost = obj.cost?.() || {};
  const reward = obj.get?.() || {};

  // Verifica se tem recursos suficientes
  for (let res in cost) {
    if ((inv.resources[res] ?? 0) < cost[res]) {
      console.log(`Not enough ${res}!`);
      return;
    }
  }

  // Deduz custo
  for (let res in cost) {
    inv.resources[res] -= cost[res];
  }

  // Aplica recompensas
  for (let key in reward) {
    if (inv.resources.hasOwnProperty(key)) {
      inv.resources[key] += reward[key];
    } else {
      inv.buildings[key] = (inv.buildings[key] || 0) + reward[key];
    }
  }

  console.log(obj.buildMsg);
  draw();
}


// ===============================
// Atualiza o painel de recursos
// ===============================
function draw() {
  const Display = document.querySelector(".resources");
  if (!Display) return;

  let text = "Resources:\n";
  for (let res in inv.resources) text += `${res}: ${inv.resources[res]}\n`;

  if (Object.keys(inv.buildings).length) {
    text += "\nBuildings:\n";
    for (let b in inv.buildings) text += `${b}: ${inv.buildings[b]}\n`;
  }

  Display.innerText = text;
  checkUnlocks();
}


// ===============================
// Verifica e exibe botões desbloqueáveis
// ===============================
function checkUnlocks() {
  const FirePanel = document.getElementById("FirePanel");
  if (!FirePanel) return;

  const existingButtons = new Set(
    Array.from(FirePanel.children).map(btn => btn.dataset.key)
  );

  for (let type in Fire) {
    for (let key in Fire[type]) {
      const obj = Fire[type][key];
      const requirements = obj.requirements?.() || obj.cost?.() || {};

      let canUnlock = Object.entries(requirements)
        .every(([res, amount]) => (inv.resources[res] ?? 0) >= amount);

      if (canUnlock && !existingButtons.has(key)) {
        const btn = document.createElement("button");
        btn.innerText = obj.name;
        btn.dataset.key = key;
        btn.onclick = () => checkAction(obj);
        FirePanel.appendChild(btn);

        if (obj.availableMsg) console.log(obj.availableMsg);
      }
    }
  }
}

draw();
