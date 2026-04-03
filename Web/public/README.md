# Jogos Ambientais - Static Website

This is a pure HTML, CSS, and JavaScript version of the Jogos Ambientais website. No build process or framework required!

## 📁 Files

- **index.html** - Homepage with hero section, importance content, and impact statistics
- **recursos.html** - Resources page for future educational content
- **navigation.js** - JavaScript for navigation and smooth scrolling

## 🚀 How to Use

### Option 1: Open Directly in Browser
Simply double-click `index.html` or `recursos.html` to open in your default browser.

### Option 2: Local Web Server (Recommended)
For the best experience, serve the files with a local web server:

**Using Python:**
```bash
cd public
python -m http.server 8000
```
Then visit: http://localhost:8000

**Using Node.js (http-server):**
```bash
npm install -g http-server
cd public
http-server
```

**Using PHP:**
```bash
cd public
php -S localhost:8000
```

## 🎨 Styling

The website uses **Tailwind CSS** via CDN, so no CSS compilation needed. All styles are embedded in the HTML files using Tailwind utility classes.

## ✨ Features

- ✅ Fully responsive design
- ✅ Smooth scrolling
- ✅ Active navigation highlighting
- ✅ Animated emojis and transitions
- ✅ Gradient backgrounds
- ✅ No dependencies (except Tailwind CDN)

## 📱 Browser Compatibility

Works in all modern browsers:
- Chrome
- Firefox
- Safari
- Edge

## 🌐 Deployment

You can deploy these files to any static hosting service:

- **GitHub Pages**: Push to a repo and enable GitHub Pages
- **Netlify**: Drag and drop the `public` folder
- **Vercel**: Deploy as a static site
- **Surge**: `surge public` (after installing surge-cli)
- **Amazon S3**: Upload to S3 bucket with static hosting enabled

## 📝 Editing

To make changes:

1. Open the HTML files in any text editor
2. Edit the content, classes, or structure
3. Refresh your browser to see changes
4. No build process needed!

## 🎯 Structure

```
public/
├── index.html          # Homepage
├── recursos.html       # Resources page
├── navigation.js       # Navigation functionality
└── README.md          # This file
```

## 💡 Tips

- All images are loaded from Unsplash CDN
- Tailwind CSS is loaded from CDN (no local CSS files needed)
- The JavaScript is vanilla JS (no frameworks)
- Navigation active states update automatically based on current page

---

© 2024 Jogos Ambientais. Cada jogo faz a diferença. 🎮🍃
