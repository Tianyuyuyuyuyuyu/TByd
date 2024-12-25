import requests
from bs4 import BeautifulSoup
import json
from datetime import datetime

def is_game_related(description):
    keywords = ['game', 'unity', 'unreal', 'pygame', 'gamedev', 'gaming',
               'game-engine', '游戏', '游戏引擎', 'godot', 'opengl', 'directx']
    if description:
        return any(keyword in description.lower() for keyword in keywords)
    return False

def fetch_trending():
    url = "https://github.com/trending"
    response = requests.get(url)
    soup = BeautifulSoup(response.text, 'html.parser')
    
    repos = []
    articles = soup.select('article.Box-row')
    
    for article in articles[:20]:
        try:
            name = article.select_one('h2 a').get_text(strip=True)
            description = article.select_one('p')
            description = description.get_text(strip=True) if description else ""
            url = f"https://github.com/{name}"
            
            if is_game_related(description):
                repos.append({
                    'name': name,
                    'description': description,
                    'url': url
                })
        except:
            continue
    
    return repos

# 获取趋势项目
trending_repos = fetch_trending()

# 保存结果
current_time = datetime.now().strftime('%Y-%m-%d %H:%M')

with open(f'Action_Trending/trending-{datetime.now().strftime("%Y%m%d%H%M")}.md', 'w', encoding='utf-8') as f:
    f.write(f'# GitHub 游戏开发趋势项目 ({current_time})\n\n')
    if trending_repos:
        for repo in trending_repos:
            f.write(f'## {repo["name"]}\n')
            f.write(f'- 描述：{repo["description"]}\n')
            f.write(f'- 链接：{repo["url"]}\n\n')
    else:
        f.write('今天没有发现游戏相关的热门项目。\n')
