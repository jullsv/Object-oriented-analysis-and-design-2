from visitors.file_visitor import FileVisitor
from elements.image_file import ImageFile
from elements.document_file import DocumentFile
import os

class ConvertToJPEGVisitor(FileVisitor):
    def __init__(self, output_path: str, quality: int = 90):
        super().__init__(output_path)
        self._quality = quality
    
    def visit_image_file(self, file: ImageFile) -> None:
        print(f"Конвертация изображения {file.path} в JPEG")
        print(f"   Параметры: {file.width}x{file.height}, формат {file.format}")
        print(f"   Качество: {self._quality}%")
        
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(self._output_path, f"{name_without_ext}.jpg")
        
        print(f"   Сохранение в: {output_file}")
        
        try:
            from PIL import Image
            img = Image.open(file.path)
            if img.mode in ('RGBA', 'P'):
                img = img.convert('RGB')
            img.save(output_file, 'JPEG', quality=self._quality)
            print(f"   Изображение конвертировано в JPEG")
        except ImportError:
            print("   Установите Pillow: pip install Pillow")
            with open(output_file, 'w', encoding='utf-8') as f:
                f.write(f"JPEG конвертация: {file.path}\n")
                f.write(f"Качество: {self._quality}%\n")
            print(f"   Создан файл: {output_file}")
        except Exception as e:
            print(f"   Ошибка: {e}")
    
    def visit_document_file(self, file: DocumentFile) -> None:
        print(f"Конвертация документа {file.path} в JPEG")
        print(f"   Параметры: {file.pages} стр., {file.word_count} слов")
        print(f"   Качество: {self._quality}%")
        
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(self._output_path, f"{name_without_ext}.jpg")
        
        print(f"   Сохранение в: {output_file}")
        
        with open(output_file, 'w', encoding='utf-8') as f:
            f.write(f"Документ конвертирован в JPEG\n")
            f.write(f"Исходный файл: {file.path}\n")
            f.write(f"Страниц: {file.pages}\n")
            f.write(f"Слов: {file.word_count}\n")
            f.write(f"Качество: {self._quality}%\n")
        
        print(f"   Создан файл: {output_file}")